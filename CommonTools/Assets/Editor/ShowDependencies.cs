using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;
using System.IO;

public class ShowDependencies : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Assets/Show Dependencies")]
    public static void Init()
    {
        GetWindow<ShowDependencies>();
    }

    public static bool CheckHasChinese(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            char ch = str[i];
            if (ch >= 0x4e00 && ch <= 0xFEFE)
                return true;
        }

        return false;
    }

    static string[] WhiteShaderList = new string[] { "Shader Forge" };
    public static Dictionary<string, Shader> shaderList = new Dictionary<string, Shader>();

    [MenuItem("Tools/SearchShaders")]
    public static void SearchShaders()
    {
        shaderList.Clear();
        var shaderfiles = Directory.GetFiles(Application.dataPath + "/Shaders", "*.shader", SearchOption.AllDirectories);
        foreach (var file in shaderfiles)
        {
            var resFilePath = "Assets" + file.Replace(Application.dataPath, "");
            resFilePath = resFilePath.Replace("\\", "/");
            Shader shader = AssetDatabase.LoadAssetAtPath(resFilePath, typeof(Shader)) as Shader;
            if (shader == null)
            {
                Debug.LogError(file);
                return;
            }
            if (!shaderList.ContainsValue(shader))
            {
                if (shaderList.ContainsKey(shader.name))
                {
                    Debug.LogError(shader.name);
                    continue;
                }
                shaderList.Add(shader.name, shader);
            }
        }
        foreach (var shaderfile in shaderList.Keys)
        {
            Debug.Log(shaderfile);
        }
    }

    [MenuItem("Tools/ReplacePrefabBuildInShader")]
    public static void ReplaceBuildInShader()
    {
        var prefabs = Selection.objects;
        foreach (var prefab in prefabs)
        {
            //if (prefab.GetType()==typeof(PrefabType))
            {
                var depend = AssetDatabase.GetDependencies(new string[] { AssetDatabase.GetAssetPath(prefab) });
                foreach (var d in depend)
                {
                    if (d.EndsWith(".mat"))
                    {
                        var mat = AssetDatabase.LoadAssetAtPath(d, typeof(Material)) as Material;
                        if (mat == null)
                        {
                            Debug.LogError("mat can not find " + d);
                            return;
                        }
                        Shader shader = mat.shader;
                        if (shaderList.ContainsKey(shader.name))
                        {
                            if (mat.shader != shaderList[shader.name])
                            {
                                Debug.LogWarning("replayce shader mat" + mat.name + "shader===>>" + shader.name);
                                mat.shader = shaderList[shader.name];
                            }
                        }
                    }
                }
            }
        }
    }

    public static List<string> DeleteParentFolders = new List<string>() { "/SceneFolder/Scenes/Scenes" };
    public static List<string> DeleteFolders = new List<string>() {"Materials"};
    public static List<string> DeleteDirParentFolders = new List<string>() { "Prefabs", "prefabs" };
    public static List<string> FolderToDelete = new List<string>();
    [MenuItem("Tools/DeletRedundantResources")]
    public static void DeletRedundantResources()
    {
        foreach (var parent in DeleteParentFolders)
        {
            string fullpath = Application.dataPath + parent;
            RecursiveSelectFolders(fullpath, DeleteFolders);
        }
        foreach (var folder in FolderToDelete)
        {
            AssetDatabase.DeleteAsset(folder.Replace(Application.dataPath,"Assets"));
        }
        AssetDatabase.Refresh();
    }

    static void RecursiveSelectFolders(string path, List<string> searchFilters)
    {
        string[] dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs)
        {
            string foldername = Path.GetFileNameWithoutExtension(dir);
            string parentFolderName = Path.GetFileNameWithoutExtension (Path.GetDirectoryName(dir));//父文件夹
            if (DeleteFolders.Contains(foldername) && DeleteDirParentFolders.Contains(parentFolderName))
            {
                FolderToDelete.Add(dir);
            }
            RecursiveSelectFolders(dir, searchFilters);
        }
    }
    [MenuItem("Tools/CheckEmptyPrefabAssetbundleName")]
    public static void CheckPrefabAssetbundleName()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        path = Application.dataPath + path.Replace("Assets","");
        files.Clear();
        RecursiveSelectChildren(path);
        foreach (var d in files)
        {
            if (AssetImporter.GetAtPath(d.Replace(Application.dataPath, "Assets")).assetBundleName == null)
                Debug.LogError(d);
        }
    }

    static List<string> files = new List<string>();
    static void RecursiveSelectChildren(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Contains(".meta")) continue;
            if (!names[i].Contains(".prefab")) continue;
            names[i] = names[i].Replace("\\", "/");
            files.Add(names[i]);
        }
        foreach (string dir in dirs)
        {
            RecursiveSelectChildren(dir);
        }
    }
    [MenuItem("Tools/FindStandardShader")]
    public static void FindStandardShader()
    {
        List<Object> selects = new List<Object>();
        Object[] objs = Selection.objects;
        for (int i = 0; i < objs.Length; i++)
        {
            Object o = objs[i];
            string name = AssetDatabase.GetAssetPath(objs[i]);
            if (name.EndsWith(".mat"))
            {
                var mat = AssetDatabase.LoadAssetAtPath(name, typeof(Material)) as Material;
                if (mat == null)
                {
                    Debug.LogError("mat can not find " + name);
                    return;
                }
                Shader shader = mat.shader;
                if (shader.name.ToLower().Contains("standard"))
                {
                    selects.Add(o);
                }
            }
        }
        Selection.objects = selects.ToArray();
    }

    static List<Object> publics = new List<Object>();
    [MenuItem("Assets/CheckPublicResource")]
    public static void CheckPubResource()
    {
        publics.Clear();
        Object[] objects = Selection.objects;
        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = i + 1; j < objects.Length; j++)
            {
                GetPubOfTwo(objects[i],objects[j]);
            }
        }
        Selection.objects = publics.ToArray();
    }

    static void GetPubOfTwo(Object obj1, Object obj2)
    {
        var dep1 = AssetDatabase.GetDependencies(new string[] { AssetDatabase.GetAssetPath(obj1)});
        var dep2 = AssetDatabase.GetDependencies(new string[] { AssetDatabase.GetAssetPath(obj2)});
        foreach (var d1 in dep1)
        {
            foreach (var d2 in dep2)
            {
                if (d1 == d2)
                {
                    //if (Path.GetExtension(d1).ToLower().Equals(".shader")) continue;
                    if (Path.GetExtension(d1).ToLower().Equals(".cs")) continue;
                    publics.Add(AssetDatabase.LoadAssetAtPath(d1,typeof(Object)));
                }
            }
        }
    }
}

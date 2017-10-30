//先选中  设置 名字
//Test/Save Assetbundle File Name to txt把需要设置的保存到txt里面
//需要单独导出资源的时候 先 "Test/Remove All AssetBundle Name in txt" 把所有name的去掉
//下次需要全部倒出的时候 "Test/Save Assetbundle File Name to txt"  把所有的name按照txt赋值
//每次需要更改txt之前先"Test/Output all paths" 给testfiles赋值，再  执行添加或者去掉name的操作，然后 再执行"Test/Save Assetbundle File Name to txt"操作
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleBuildNew : MonoBehaviour
{
    public static string AssetbundlePrefix;
    public static bool isUI = false;
    const string SetAssetbundlePrefix_character = "BuildAssetbundle/character";
    const string SetAssetbundlePrefix_particles = "BuildAssetbundle/particle";
    const string SetAssetbundlePrefix_texture = "BuildAssetbundle/texture";
    const string SetAssetbundlePrefix_scene = "BuildAssetbundle/scene";
    const string SetAssetBundlePrefix_audio = "BuildAssetbundle/audio";
    const string SetAssetBundlePrefix_cutscene = "BuildAssetbundle/cutsce";
    const string SetAssetBundlePrefix_shader = "BuildAssetbundle/shader";

    public static readonly string ABDirPC = "assetbundles";
    public static readonly string ABDirAndroid = "assetbundles_android";
    public static readonly string ABDirIphone = "assetbundles_iphone";
    public static List<string> testFiles = new List<string>();
    public static string ABSuffix = ".unity3d";
    /// <summary>
    /// 记录打出的资源
    /// </summary>
    #region 
    //[MenuItem("Test/Output all paths")]
    public static void outputAllpaths()
    {
        GetAllAssetBundlePath();
        foreach (var path in testFiles)
        {
            Debug.Log(path);
        }
    }

    public static void GetAllAssetBundlePath()
    {
        testFiles.Clear();
        TextAsset pathTxt = AssetDatabase.LoadAssetAtPath("Assets/AllAssetbundlepath.txt", typeof(TextAsset)) as TextAsset;
        if (pathTxt == null)
        {
            Debug.LogError("you have not save file info");
            return;
        }
        string[] filepaths = pathTxt.text.Split('|');
        foreach (var file in filepaths)
        {
            //			if(!file.IsNullOrEmpty())
            testFiles.Add(file);
        }
    }

    //[MenuItem("Test/Save Assetbundle File Name to txt")]
    public static void SaveAssetbundleNameToTxt()
    {
        string f = "";
        for (int i = 0; i < testFiles.Count; i++)
        {
            if (i != testFiles.Count - 1)
                f += testFiles[i] + "|";
            else
                f += testFiles[i];
        }
        //		foreach (var item in testFiles)
        //		{
        //			f += item + "|";
        //		}
        System.IO.File.WriteAllText("Assets" + "/AllAssetbundlepath.txt", f);
        AssetDatabase.Refresh();
    }
    //[MenuItem("Test/Remove All AssetBundle Name in txt")]
    public static void RemoveAllAssetBundleName()
    {
        GetAllAssetBundlePath();
        foreach (var item in testFiles)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(item, typeof(Object));
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = null;
        }
    }
    //[MenuItem("Test/Add All AssetBundle Name in txt")]
    public static void AddAllAssetBundleName()
    {
        GetAllAssetBundlePath();
        foreach (var item in testFiles)
        {

            Object obj = AssetDatabase.LoadAssetAtPath(item, typeof(Object));
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = obj.name + ".unity3d";
        }
    }
    #endregion
    [MenuItem(SetAssetbundlePrefix_character)]
    public static void SetAssetbundlepreCharacter()
    {
        AssetbundlePrefix = "character";
        CheckMenu();
    }
    [MenuItem(SetAssetbundlePrefix_particles)]
    public static void SetAssetbundlepreParticle()
    {
        AssetbundlePrefix = "particle";
        CheckMenu();
    }
    [MenuItem(SetAssetbundlePrefix_texture)]
    public static void SetAssetbundlepretexure()
    {
        AssetbundlePrefix = "texture";
        CheckMenu();
    }
    [MenuItem(SetAssetbundlePrefix_scene)]
    public static void SetAssetbundleprescenee()
    {
        AssetbundlePrefix = "scene";
        CheckMenu();
    }
    [MenuItem(SetAssetBundlePrefix_audio)]
    public static void SetAssetbundlepreaudio()
    {
        AssetbundlePrefix = "audio";
        CheckMenu();
    }
    [MenuItem(SetAssetBundlePrefix_cutscene)]
    public static void SetAssetbundleprecutscene()
    {
        AssetbundlePrefix = "cutsce";
        CheckMenu();
    }
    [MenuItem(SetAssetBundlePrefix_shader)]
    public static void SetAssetbundlepreshader()
    {
        AssetbundlePrefix = "shader";
        CheckMenu();
    }
    static void CheckMenu()
    {
        Menu.SetChecked(SetAssetbundlePrefix_texture, AssetbundlePrefix.Equals("texture"));
        Menu.SetChecked(SetAssetbundlePrefix_character, AssetbundlePrefix.Equals("character"));
        Menu.SetChecked(SetAssetbundlePrefix_particles, AssetbundlePrefix.Equals("particle"));
        Menu.SetChecked(SetAssetbundlePrefix_scene, AssetbundlePrefix.Equals("scene"));
        Menu.SetChecked(SetAssetBundlePrefix_audio, AssetbundlePrefix.Equals("audio"));
        Menu.SetChecked(SetAssetBundlePrefix_cutscene, AssetbundlePrefix.Equals("cutsce"));
        Menu.SetChecked(SetAssetBundlePrefix_cutscene, AssetbundlePrefix.Equals("shader"));
    }

    //打包资源
    const string uimenu = "BuildAssetbundle/Build UI";
    [MenuItem("BuildAssetbundle/Build UI")]
    public static void SetBuilModeUI()
    {
       
    }
    [MenuItem(uimenu)]
    public static void ToggleSimulateAssetBundle()
    {
        isUI = !isUI;
    }

    [MenuItem(uimenu, true)]
    public static bool ToggleSimulateAssetBundleValidate()
    {
        Menu.SetChecked(uimenu, isUI);
        return true;
    }
    [MenuItem("BuildAssetbundle/Build AssetBundles PC 5")]
    static public void BuildAssetBundlesPC()
    {
        BuildAssetBundlesNew(BuildTarget.StandaloneWindows);
    }
    [MenuItem("BuildAssetbundle/Build AssetBundles Andriod 5")]
    static public void BuildAssetBundlesAndriod()
    {
        BuildAssetBundlesNew(BuildTarget.Android);
    }

    [MenuItem("BuildAssetbundle/Build AssetBundles IOS 5")]
    static public void BuildAssetBundlesIOS()
    {
        BuildAssetBundlesNew(BuildTarget.iOS);
    }
    [MenuItem("BuildAssetbundle/DeleteManifest")]
    static public void DeleteManifest()
    {
        files.Clear();
        string path = GetAssetBundlePath();
        if (isUI)
            path += "ui";
        Recursive(Application.dataPath + "/../"+ path);
        foreach(var file in files)
        {
            if (Path.GetExtension(file).Equals(".manifest"))
                File.Delete(file);
        }
    }
    [MenuItem("BuildAssetbundle/DeleteManifestInStreammingAsset")]
    static public void DeleteManifestInStreammingAsset()
    {
        files.Clear();
        string path1 = ABDirPC;
        Recursive(Application.streamingAssetsPath + "/" + path1);
        string path2 = ABDirAndroid;
        Recursive(Application.streamingAssetsPath + "/" + path2);
        //string path3 = ABDirIphone;
        //Recursive(Application.streamingAssetsPath + "/" + path3);
        foreach (var file in files)
        {
            if (Path.GetExtension(file).Equals(".manifest"))
                File.Delete(file);
        }
        AssetDatabase.Refresh();
    }
    public static string GetAssetBundlePath()
    {
        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.StandaloneWindows:
                return ABDirPC;
            case BuildTarget.Android:
                return ABDirAndroid;
            case BuildTarget.iOS:
                return ABDirIphone;
            default:
                return null;
        }
    }
    [MenuItem("Assets/CheckbackSpace")]
    public static void CheckSpaceBack()
    {
        Object[] objects = Selection.objects;
        List<Object> objectss = new List<Object>();
        foreach (var obj in objects)
        {
            if (obj.name.IndexOf(" ") > 0)
                objectss.Add(obj);
        }
        Selection.objects = objectss.ToArray();
    }

    [MenuItem("Assets/SetAssetBundleName")]
    public static void SetAssetBundleName()
    {
        if (string.IsNullOrEmpty(AssetbundlePrefix))
        {
            Debug.LogError("请到BuildAssetbundle Menu下选择assetbundle所属的类型");
            return;
        }
        Object[] objects = Selection.objects;
        foreach (var obj in objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (Path.GetExtension(path).ToLower().Equals(".cs")) continue;
            if (obj.name.Contains(" ")) Debug.LogWarning("名称 存在 空格" + obj.name);
            //if (AssetbundlePrefix.ToLower().Equals("particle") && (Path.GetExtension(path).ToLower().Equals(".controller") || Path.GetExtension(path).ToLower().Equals(".anim"))) continue;
            if (Path.GetExtension(path).Equals(".shader"))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "shader" + "/" + obj.name.Replace(" ", "-") + Path.GetExtension(path)+ ABSuffix;
            else if(AssetbundlePrefix.ToLower().Equals("texture") &&(Path.GetExtension(path).ToLower().Equals(".png") || Path.GetExtension(path).ToLower().Equals(".tga") || Path.GetExtension(path).ToLower().Equals(".jpg")))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "texture" + "/" + obj.name.Replace(" ", "-") + ABSuffix;
            else if(AssetbundlePrefix.ToLower().Equals("scene") && Path.GetExtension(path).Equals(".unity"))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "scene" + "/" + obj.name.Replace(" ", "-")  + ABSuffix;
            else if (AssetbundlePrefix.ToLower().Equals("audio"))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "audio" + "/" + obj.name.Replace(" ", "-") + ABSuffix;
            else if (AssetbundlePrefix.ToLower().Equals("cutsce"))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "cutsce" + "/" + obj.name.Replace(" ", "-") + ABSuffix;
            else if(AssetbundlePrefix.ToLower().Equals("shader"))
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = "shader" + "/" + obj.name.Replace(" ", "-") + ABSuffix;
            else
                AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = AssetbundlePrefix + "/" + obj.name.Replace(" ","-") + Path.GetExtension(path)+ABSuffix;
            //Debug.LogWarning(AssetDatabase.GetAssetPath(obj));
            testFiles.Add(AssetDatabase.GetAssetPath(obj));
        }
        //		string assetBundleName = Selection.activeObject.name;
        //		AssetImporter.GetAtPath (AssetDatabase.GetAssetPath (Selection.activeObject)).assetBundleName = assetBundleName+".unity3d";
    }
    //[MenuItem("Test/SetAssetBundleNameInTheFile")]
    public static void SetAssetBundleNameInTheFile()
    {
        var select = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(select);
        if (path.Contains(".")) return;
        Recursive(path);
        foreach (var item in files)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(item, typeof(Object));
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = path + "/" + obj.name + ".unity3d";
            Debug.LogWarning(AssetDatabase.GetAssetPath(obj));
            testFiles.Add(AssetDatabase.GetAssetPath(obj));
        }
        files.Clear();
    }

    static List<string> files = new List<string>();
    //get all Objects's path in the file included sub file's Objects
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Contains(".meta")) continue;
            names[i] = names[i].Replace("\\", "/");
            files.Add(names[i]);
        }
        foreach (string filename in names)
        {
            if (filename.Contains(".meta")) continue;
            files.Add(filename);
        }
        foreach (string dir in dirs)
        {
            Recursive(dir);
        }
    }
    [MenuItem("Assets/RemoveAssetBundleName")]
    public static void RemoveAssetBundleName()
    {
        Object[] objects = Selection.objects;
        foreach (var obj in objects)
        {
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = null;
            Debug.LogWarning(AssetDatabase.GetAssetPath(obj));
            testFiles.Remove(AssetDatabase.GetAssetPath(obj));
        }
        // 		AssetImporter.GetAtPath (AssetDatabase.GetAssetPath (Selection.activeObject)).assetBundleName = null;
    }
    [MenuItem("BuildAssetbundle/RemoveAssetBundleNameInTheFile")]
    public static void RemoveAssetBundleNameInTheFile()
    {
        var select = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(select);
        if (path.Contains(".")) return;
        Recursive(path);
        foreach (var item in files)
        {
            Object obj = AssetDatabase.LoadAssetAtPath(item, typeof(Object));
            AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(obj)).assetBundleName = null;
            Debug.LogWarning(AssetDatabase.GetAssetPath(obj));
            testFiles.Remove(AssetDatabase.GetAssetPath(obj));
        }
        files.Clear();
    }

    [MenuItem("BuildAssetbundle/检测一个AssetbundleName是否多用")]
    public static void CheckMultiUsedAssetbundleName()
    {
        string[] allAssetbundleNames = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < allAssetbundleNames.Length; i++)
        {
            string[] paths = AssetDatabase.GetAssetPathsFromAssetBundle(allAssetbundleNames[i]);
            if (paths != null && paths.Length > 1)
            {
                Debug.LogError(allAssetbundleNames[i] + "===名字被复用");
            }
        }
    }

    const string kAssetBundlesOutputPath = "AssetBundles";

    public static void BuildAssetBundlesNew(BuildTarget platform,bool needOpen=true)
    {
        // Choose the output path according to the build target.

        ShowDependencies.DeletRedundantResources();

        string outputPath = GetPlatformFolderForAssetBundlesNew(platform);
        if (isUI)
            outputPath += "ui";
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);
        Debug.Log("输出路径" + outputPath);
        BuildPipeline.BuildAssetBundles(outputPath, 0, platform);
        if (needOpen)
        {
            Debug.LogError("导出结束");
            System.Diagnostics.Process.Start(outputPath, outputPath);
        }
    }

#if UNITY_EDITOR
    public static string GetPlatformFolderForAssetBundlesNew(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return ABDirAndroid;
            case BuildTarget.iOS:
                return ABDirIphone;
            case BuildTarget.WebPlayer:
                return "AssetBundle5/WebPlayer/";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return ABDirPC;
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "AssetBundle5/OSX/";
            // Add more build targets for your own.
            // If you add more targets, don't forget to add the same platforms to GetPlatformFolderForAssetBundles(RuntimePlatform) function.
            default:
                return null;
        }
    }
#endif
    public class MyAllPostprocessor : AssetPostprocessor
    {

        public void OnPostprocessAssetbundleNameChanged(string assetPath, string previousAssetBundleName, string newAssetBundleName)
        {
            Debug.Log("Asset " + assetPath + " has been moved from assetBundle " + previousAssetBundleName + " to assetBundle " + newAssetBundleName + ".");
        }
    }
}

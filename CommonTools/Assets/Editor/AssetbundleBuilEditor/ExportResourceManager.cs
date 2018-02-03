using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class RefObjpair
{
    public string src1;
    public string src2;

    public RefObjpair(string s1, string s2)
    {
        src1 = s1;
        src2 = s2;
    }
}

public class PubAssetRecord
{
    public string assetpath;
    public Object obj;
    public int refSrcCount = 0;
    public bool assetself = false;
    public bool isShader;
    public List<RefObjpair> refspair = new List<RefObjpair>();

    //添加引用次数
    public void AddRef(bool self)
    {
        if (!self && !isShader)
            refSrcCount++;
    }

    public PubAssetRecord(string name, Object obj, bool self, bool isshader)
    {
        this.assetpath = name;
        this.obj = obj;
        this.assetself = self;
        this.isShader = isshader;
        refSrcCount++;
        refspair.Add(new RefObjpair(name, ""));
    }

    public List<string> allRefObjs = new List<string>();
    public void AddToSrc(string _src)
    {
        if (!allRefObjs.Contains(_src))
            allRefObjs.Add(_src);
    }

    public int getAllRefCount()
    {
        return allRefObjs.Count;
    }
}

public class ExportResourceManager : EditorWindow
{
    static ExportResourceInfo[] exportinfos;
    static string ExporInfoAssetpath = "/Editor/AssetbundleBuilEditor/ExportInfo.txt";

    [MenuItem("ExportAssetbundle/ExportResource")]
    public static void ExportResourceWindow()
    {
        InitialExportInfo();
        var window = EditorWindow.GetWindow(typeof(ExportResourceManager)) as ExportResourceManager;
        window.minSize = new Vector2(800, 300);
        window.Show();
    }

    static void InitialExportInfo()
    {
        exportinfos = new ExportResourceInfo[] { };
        GetExportInfor();
        for (int i = 0; i < exportinfos.Length; i++)
        {
            if (!resourceFilters.ContainsKey(exportinfos[i].exportrootpath))
            {
                resourceFilters[exportinfos[i].exportrootpath] = new List<string>();
            }
            if (!string.IsNullOrEmpty(exportinfos[i].filter1))
            {
                resourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].filter1);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].filter1))
            {
                resourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].filter2);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].filter1))
            {
                resourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].filter3);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].filter1))
            {
                resourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].filter4);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].filter1))
            {
                resourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].filter5);
            }
        }
        for (int i = 0; i < exportinfos.Length; i++)
        {
            if (!searchresourceFilters.ContainsKey(exportinfos[i].exportrootpath))
            {
                searchresourceFilters[exportinfos[i].exportrootpath] = new List<string>();
            }
            if (!string.IsNullOrEmpty(exportinfos[i].extensions1))
            {
                searchresourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].extensions1);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].extensions2))
            {
                searchresourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].extensions2);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].extensions3))
            {
                searchresourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].extensions3);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].extensions4))
            {
                searchresourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].extensions4);
            }
            if (!string.IsNullOrEmpty(exportinfos[i].extensions5))
            {
                searchresourceFilters[exportinfos[i].exportrootpath].Add(exportinfos[i].extensions5);
            }
        }

    }

    static void GetExportInfor()
    {
        exportinfos = CsvImporter.Parser<ExportResourceInfo>(GetDataBaseBytes(ExporInfoAssetpath));
    }
    private static byte[] GetDataBaseBytes(string textName)
    {

        return System.IO.File.ReadAllBytes(Application.dataPath + ExporInfoAssetpath);
    }

    public Vector2 scrollPosition;
    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        DrawSeparator();
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        GUILayout.Label("Index", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("resourcePath", GUILayout.Width(200));
        GUILayout.Space(10);
        GUILayout.Label("extensions1", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("extensions2", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("extensions3", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("extensions4", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("extensions5", GUILayout.Width(50));
        GUILayout.Space(10);
        GUILayout.Label("exportrootpath", GUILayout.Width(200));
        GUILayout.Space(10);
        GUILayout.Label("filter1", GUILayout.Width(40));
        GUILayout.Space(10);
        GUILayout.Label("filter2", GUILayout.Width(40));
        GUILayout.Space(10);
        GUILayout.Label("filter3", GUILayout.Width(40));
        GUILayout.Space(10);
        GUILayout.Label("filter4", GUILayout.Width(40));
        GUILayout.Space(10);
        GUILayout.Label("filter5", GUILayout.Width(40));
        GUILayout.Space(10);
        GUILayout.Label("SetName Min PubRef", GUILayout.Width(40));
        GUILayout.EndHorizontal();

        if (exportinfos == null) return;
        for (int i = 0; i < exportinfos.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].index.ToString(), GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].resourcePath, GUILayout.Width(200));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].extensions1, GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].extensions2, GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].extensions3, GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].extensions4, GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].extensions5, GUILayout.Width(50));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].exportrootpath, GUILayout.Width(200));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].filter1, GUILayout.Width(40));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].filter2, GUILayout.Width(40));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].filter3, GUILayout.Width(40));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].filter4, GUILayout.Width(40));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].filter5, GUILayout.Width(40));
            GUILayout.Space(10);
            GUILayout.Label(exportinfos[i].MinSetnameRef.ToString(), GUILayout.Width(40));
            GUILayout.Space(20);
            if (GUILayout.Button("更新依赖", GUILayout.Width(150)))
            {
                UpdateDependency(Application.dataPath + exportinfos[i].resourcePath, exportinfos[i].exportrootpath, searchresourceFilters[exportinfos[i].exportrootpath],
                    resourceFilters[exportinfos[i].exportrootpath], exportinfos[i].MinSetnameRef);
            }
            GUILayout.EndHorizontal();
        }

        DrawSeparator();
        if (GUILayout.Button("一键导出资源", GUILayout.Width(120f)))
        {
            AssetBundleBuildEditor.BuildAssetBundlesNew(EditorUserBuildSettings.activeBuildTarget);
        }
        GUILayout.EndScrollView();
    }

    //记录公用的次数
    static Dictionary<string, PubAssetRecord> pubassetrecords = new Dictionary<string, PubAssetRecord>(1000);
    static List<Object> publicsFilterByRef = new List<Object>(1000);

    //记录每次更新依赖找到的共有文件
    static List<string> publicsNames = new List<string>(1000);
    //记录和当前种类相关的已存在的依赖名字
    static List<string> relatedAssetbundleName = new List<string>(1000);
    //存放选中的物体
    static List<Object> selectedObjects = new List<Object>(1000);
    //存在当前文件夹下所有的对应后缀的子物体。
    static List<string> files = new List<string>(1000);
    //存放查找的后缀名
    static Dictionary<string, List<string>> searchresourceFilters = new Dictionary<string, List<string>>(1000);
    //存放查找依赖时需要过滤的后缀名
    static Dictionary<string, List<string>> resourceFilters = new Dictionary<string, List<string>>(1000);

    static void UpdateDependency(string resourcePath, string exportrootpath, List<string> searchFilters, List<string> dependencyfilters, int SetnameMimRef, bool needOpen = true)
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        files.Clear();
        selectedObjects.Clear();
        CollectRelatedAssetbundle(exportrootpath);
        RecursiveSelectChildren(resourcePath, searchFilters);
        foreach (var item in files)
        {
            selectedObjects.Add(AssetDatabase.LoadAssetAtPath(item.Replace(Application.dataPath, "Assets"), typeof(Object)));
        }
        Selection.objects = selectedObjects.ToArray();
        CheckPubResource(dependencyfilters, SetnameMimRef, exportrootpath, needOpen);
        SetAssetbundlePrefix(exportrootpath);
        AssetBundleBuildEditor.SetAssetBundleName();
        RemoveUnusedAssetbundleName(publicsNames, relatedAssetbundleName);
    }
    public static void SetAssetbundlePrefix(string exportrootpath)
    {
        switch (exportrootpath)
        {
            case "character":
                AssetBundleBuildEditor.SetAssetbundlepreCharacter();
                break;
            case "particle":
                AssetBundleBuildEditor.SetAssetbundlepreParticle();
                break;
            case "texture":
                AssetBundleBuildEditor.SetAssetbundlepretexure();
                break;
            case "scene":
                AssetBundleBuildEditor.SetAssetbundleprescenee();
                break;
            case "audio":
                AssetBundleBuildEditor.SetAssetbundlepreaudio();
                break;
            case "cutsce":
                AssetBundleBuildEditor.SetAssetbundleprecutscene();
                break;
        }
    }
    static void CollectRelatedAssetbundle(string prefix)
    {
        relatedAssetbundleName.Clear();
        relatedAssetbundleName = AssetDatabase.GetAllAssetBundleNames().Where(obj => obj.Contains(prefix + "/")).ToArray().ToList();
    }
    public static void CheckPubResource(List<string> filters, int SetNameMinRef, string type, bool needOpen)
    {
        publicsNames.Clear();
        pubassetrecords.Clear();
        publicsFilterByRef.Clear();
        Object[] objects = Selection.objects;
        if (objects.Length == 1)
        {
            string assetpath = AssetDatabase.GetAssetPath(objects[0]);
            AddToPubRecord(AssetDatabase.GetAssetPath(objects[0]), objects[0], assetpath, true, false);
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                RecordRefs(objects[i], filters);
            }
        }
        WriteRefCountToTxt(pubassetrecords, type, needOpen);
        //Selection.objects = publics.ToArray();
        GetRefPublics(SetNameMinRef, type);
        Selection.objects = publicsFilterByRef.ToArray();
    }

    //获取 共用次数大于 设定的最低次数 或者是 选中物体自己 的 物体
    public static void GetRefPublics(int setnameminref, string type)
    {
        string exstion = null;
        foreach (var rec in pubassetrecords)
        {
            exstion = Path.GetExtension(rec.Value.assetpath).ToLower();
            if (type.Equals("scene") && (exstion.Equals(".png") || exstion.ToLower().Equals(".tga") || exstion.Equals(".jpg")))
            {
                Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(rec.Value.assetpath);

                if (tex == null)
                {
                    Debug.Log(rec.Value.assetpath);
                }
                if (tex.height < 256 && tex.width < 256)
                    continue;
            }
            if (type.Equals("particle") && (exstion.Equals(".png") || exstion.ToLower().Equals(".tga") || exstion.Equals(".jpg")))
            {
                Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(rec.Value.assetpath);
                if (tex == null)
                {
                    Debug.Log(rec.Value.assetpath);
                }
                if (tex.height < 256 && tex.width < 256)
                    continue;
            }
            if (type.Equals("character") && (exstion.Equals(".png") || exstion.ToLower().Equals(".tga") || exstion.Equals(".jpg")))
            {
                Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(rec.Value.assetpath);
                if (tex == null)
                {
                    Debug.Log(rec.Value.assetpath);
                }
                if (tex.height < 128 && tex.width < 128)
                    continue;
            }
            if (rec.Value.getAllRefCount() >= setnameminref || rec.Value.assetself || rec.Value.isShader)
            {
                if (!publicsFilterByRef.Contains(rec.Value.obj))
                {
                    publicsFilterByRef.Add(rec.Value.obj);
                }
                if (!publicsNames.Contains(rec.Value.assetpath))
                    publicsNames.Add(rec.Value.assetpath);
            }
        }
        Debug.LogError(publicsFilterByRef.Count + "-----筛选后的数量");
    }

    public static void WriteRefCountToTxt(Dictionary<string, PubAssetRecord> pubdic, string recordtype, bool needOpen)
    {
        StreamWriter sw = null;
        List<PubAssetRecord> tempList = new List<PubAssetRecord>();
        foreach (var pub in pubdic)
        {
            //pub.Value.GetAllRefObj();
            tempList.Add(pub.Value);
        }
        tempList.Sort(delegate(PubAssetRecord a1, PubAssetRecord a2)
        {
            if (a1.getAllRefCount() > a2.getAllRefCount())
                return 1;
            else if (a1.getAllRefCount() < a2.getAllRefCount())
                return -1;
            else
                return 0;
        });
        string path = Application.dataPath + "/../";
        try
        {
            string text = "";
            text += "filename,refCount\r\n";
            string temp = "";
            foreach (var pub in tempList)
            {
                //pub.GetAllRefObj();
                temp = pub.assetpath + "," + pub.getAllRefCount().ToString() + "\r\n";
                text += temp;
                for (int i = 0; i < pub.allRefObjs.Count; i++)
                {
                    if (!string.IsNullOrEmpty(pub.allRefObjs[i]))
                    {
                        temp = i.ToString() + "------" + pub.allRefObjs[i] + "\r\n";
                        text += temp;
                    }
                }
            }
            sw = new StreamWriter(path + recordtype + "_pubrecord.txt");
            sw.Write(text);
            sw.Close();
        }
        catch
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
        if (needOpen)
            System.Diagnostics.Process.Start(path, path);
    }

    //将物体的引用填进表里，如果该物体的引用数量大于1表示该物体收到多个物体的引用
    static void RecordRefs(Object obj1, List<string> filters)
    {
        string assetpath = AssetDatabase.GetAssetPath(obj1);
        if (!publicsNames.Contains(assetpath))
        {
            publicsNames.Add(AssetDatabase.GetAssetPath(obj1));
        }
        AddToPubRecord(assetpath, obj1, assetpath, true, false);
        var dep1 = AssetDatabase.GetDependencies(new string[] { assetpath });
        for (int i = 0; i < dep1.Length; i++)
        {
            string d1 = dep1[i];
            //shader单独导出
            if (Path.GetExtension(d1).ToLower().Equals(".shader"))
            {
                Object obj = AssetDatabase.LoadAssetAtPath(d1, typeof(Object));
                AddToPubRecord(d1, obj, assetpath, false, true);
            }
            else
            {
                //if (Path.GetExtension(d1).ToLower().Equals(".shader")) continue;
                if (Path.GetExtension(d1).ToLower().Equals(".cs")) continue;
                if (filters.Contains(Path.GetExtension(d1).ToLower())) continue;
                Object obj = AssetDatabase.LoadAssetAtPath(d1, typeof(Object));
                AddToPubRecord(d1, obj, assetpath, false, false);
            }
        }
    }

    public static void AddToPubRecord(string assetname, Object obj, string src, bool self = false, bool isShader = false)
    {
        if (pubassetrecords.ContainsKey(assetname))
        {
            pubassetrecords[assetname].AddRef(self);
            pubassetrecords[assetname].AddToSrc(src);
        }
        else
        {
            pubassetrecords[assetname] = new PubAssetRecord(assetname, obj, self, isShader);
            pubassetrecords[assetname].AddToSrc(src);
        }
    }

    static void RecursiveSelectChildren(string path, List<string> searchFilters)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Contains(".meta")) continue;
            names[i] = names[i].Replace("\\", "/");
            if (searchFilters.Contains(Path.GetExtension(names[i])))
                files.Add(names[i]);
        }
        foreach (string dir in dirs)
        {
            RecursiveSelectChildren(dir, searchFilters);
        }
    }

    static void RemoveUnusedAssetbundleName(List<string> currentnames, List<string> existnames)
    {
        List<string> currentAssetbundleNames = new List<string>();
        foreach (var obj in currentnames)
        {
            string assetname = AssetImporter.GetAtPath(obj).assetBundleName;
            if (!currentAssetbundleNames.Contains(assetname))
            {
                currentAssetbundleNames.Add(assetname);
            }
        }
        foreach (var e in existnames)
        {
            if (e.Contains(".shader"))
                continue;
            if (!currentAssetbundleNames.Contains(e))
            {
                Debug.LogError("强制删除多余的assetbundlename:" + e);
                AssetDatabase.RemoveAssetBundleName(e, true);
            }
        }
    }
    static public Texture2D blankTexture
    {
        get
        {
            return EditorGUIUtility.whiteTexture;
        }
    }
    static public void DrawSeparator()
    {
        GUILayout.Space(12f);

        if (Event.current.type == EventType.Repaint)
        {
            Texture2D tex = blankTexture;
            Rect rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 4f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 6f, Screen.width, 1f), tex);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 9f, Screen.width, 1f), tex);
            GUI.color = Color.white;
        }
    }
    //[MenuItem("Export/ExportResourcePC")]
    public static void ExportPC()
    {
        ExportByType(0);
    }
    public static void ExportAndroid()
    {
        ExportByType(1);
    }
    public static void ExportIOS()
    {
        ExportByType(2);
    }

    static public void ExportByType(int type)
    {
        InitialExportInfo();
        bool hasBuild = false;
        if (exportinfos == null) return;

        for (int i = 0; i < exportinfos.Length; i++)
        {
            UpdateDependency(Application.dataPath + exportinfos[i].resourcePath, exportinfos[i].exportrootpath, searchresourceFilters[exportinfos[i].exportrootpath], resourceFilters[exportinfos[i].exportrootpath], exportinfos[i].MinSetnameRef, false);
        }
        if (type == 0)
        {
            AssetBundleBuildEditor.BuildAssetBundlesNew(BuildTarget.StandaloneWindows, false);
        }
        else if (type == 1)
        {
            AssetBundleBuildEditor.BuildAssetBundlesNew(BuildTarget.Android, false);
        }
        else if (type == 2)
        {
            AssetBundleBuildEditor.BuildAssetBundlesNew(BuildTarget.iOS, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

/// <summary>
/// Editor on Unity 5.6
/// </summary>
namespace CommonTools
{
    public class CommonEditorTools : MonoBehaviour
    {

        #region 文件夹和路径相关

        public static string ClientPath = "";
        public static string AssetsPath = "";
        public static string PersistPath = "";
        public static string StreamingAsstesPath = "";
        public static string PanelPath = "";
        public static string ResoursePath = "";

        public static void InitPath()
        {
            AssetsPath = Application.dataPath;
            ClientPath = Path.GetDirectoryName(AssetsPath);
            PersistPath = Application.persistentDataPath;
            StreamingAsstesPath = Application.streamingAssetsPath;
            PanelPath = AssetsPath + "/Game_Skyfire";
            ResoursePath = AssetsPath + "/Resources";
        }

        [MenuItem("CommonTools/显示文件路径")]
        public static void ShowObjectPath()
        {
            InitPath();
            Object obj = Selection.activeObject;
            string localPath = AssetDatabase.GetAssetPath(obj);
            DebugKeyValue("AssetLocalPath", localPath);
            string FullPath = string.Format("{0}/{1}", ClientPath, localPath);
            DebugKeyValue("AssetsFullPath", FullPath);
        }

        [MenuItem("CommonTools/打开文件夹/Client目录")]
        public static void OpenClientPath()
        {
            InitPath();
            OpenFolder(ClientPath);
        }

        [MenuItem("CommonTools/打开文件夹/Assets目录")]
        public static void OpenAssetsPath()
        {
            InitPath();
            OpenFolder(AssetsPath);
        }

        [MenuItem("CommonTools/打开文件夹/持久化目录")]
        public static void OpenPersist()
        {
            InitPath();
            OpenFolder(PersistPath);
        }

        [MenuItem("CommonTools/打开文件夹/流资源目录")]
        public static void OpenStreamingAssetsPath()
        {
            InitPath();
            OpenFolder(StreamingAsstesPath);
        }

        [MenuItem("CommonTools/打开文件夹/面板目录")]
        public static void OpenPanelPath()
        {
            InitPath();
            OpenFolder(PanelPath);
        }

        [MenuItem("CommonTools/打开文件夹/Resource目录")]
        public static void OpenResourcePath()
        {
            InitPath();
            OpenFolder(ResoursePath);
        }
        #endregion

        #region common fuctions
        public static void DebugKeyValue(string key, string value)
        {
            Debug.LogError(string.Format("{0} = {1}", key, value));
        }

        public static void OpenFolder(string openPath)
        {
            if (Directory.Exists(openPath))
                System.Diagnostics.Process.Start(openPath, openPath);
        }

        /// <summary>
        /// 替换Prefab
        /// </summary>
        /// <param name="obj"></param>
        public static void ReplaceSelectPrefab(GameObject obj)
        {
            Object prefab = PrefabUtility.GetPrefabParent(obj);
            string path = AssetDatabase.GetAssetPath(prefab);
            PrefabUtility.ReplacePrefab(obj, prefab);
        }
        #endregion
    }
}



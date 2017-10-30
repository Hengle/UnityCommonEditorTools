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

        #region Texure 设置图片格式
        [MenuItem("CommonTools/Texture/Change Texture Type/Default")]
        static void ChangeTextureType_Default()
        {
            SelectedChangeTextureType(TextureImporterType.Default);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/NormalMap")]
        static void ChangeTextureType_NormalMap()
        {
            SelectedChangeTextureType(TextureImporterType.NormalMap);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/EditorGUIAndLegacyGui")]
        static void ChangeTextureType_Gui()
        {
            SelectedChangeTextureType(TextureImporterType.GUI);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Sprite")]
        static void ChangeTextureType_Sprite()
        {
            SelectedChangeTextureType(TextureImporterType.Sprite);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Cursor")]
        static void ChangeTextureType_Cursor()
        {
            SelectedChangeTextureType(TextureImporterType.Cursor);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Cookie")]
        static void ChangeTextureType_Cookie()
        {
            SelectedChangeTextureType(TextureImporterType.Cookie);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/LightMap")]
        static void ChangeTextureType_LightMap()
        {
            SelectedChangeTextureType(TextureImporterType.Lightmap);
        }
        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Texture Compression/None")]
        static void ChangeTextureCompression_None()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.Uncompressed);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/LowQuality")]
        static void ChangeTextureCompression_LowQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.CompressedLQ);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/NormalQuality")]
        static void ChangeTextureCompression_NormalQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.Compressed);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/HighQuality")]
        static void ChangeTextureCompression_HighQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.CompressedHQ);
        }
        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/32")]
        static void ChangeTextureSize_32()
        {
            SelectedChangeMaxTextureSize(32);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/64")]
        static void ChangeTextureSize_64()
        {
            SelectedChangeMaxTextureSize(64);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/128")]
        static void ChangeTextureSize_128()
        {
            SelectedChangeMaxTextureSize(128);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/256")]
        static void ChangeTextureSize_256()
        {
            SelectedChangeMaxTextureSize(256);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/512")]
        static void ChangeTextureSize_512()
        {
            SelectedChangeMaxTextureSize(512);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/1024")]
        static void ChangeTextureSize_1024()
        {
            SelectedChangeMaxTextureSize(1024);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/2048")]
        static void ChangeTextureSize_2048()
        {
            SelectedChangeMaxTextureSize(2048);
        }

        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change MipMap/Enable MipMap")]
        static void ChangeMipMap_On()
        {
            SelectedChangeMipMap(true);
        }

        [MenuItem("CommonTools/Texture/Change MipMap/Disable MipMap")]
        static void ChangeMipMap_Off()
        {
            SelectedChangeMipMap(false);
        }

        // ----------------------------------------------------------------------------


        [MenuItem("CommonTools/Texture/Change Non Power of 2/None")]
        static void ChangeNPOT_None()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.None);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToNearest")]
        static void ChangeNPOT_ToNearest()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToNearest);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToLarger")]
        static void ChangeNPOT_ToLarger()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToLarger);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToSmaller")]
        static void ChangeNPOT_ToSmaller()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToSmaller);
        }

        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Is Readable/Enable")]
        static void ChangeIsReadable_Yes()
        {
            SelectedChangeIsReadable(true);
        }

        [MenuItem("CommonTools/Texture/Change Is Readable/Disable")]
        static void ChangeIsReadable_No()
        {
            SelectedChangeIsReadable(false);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/Point")]
        static void ChangeFilterModePoint()
        {
            SelectedFilterMode(FilterMode.Point);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/BiLinear")]
        static void ChangeFilterModeBilinear()
        {
            SelectedFilterMode(FilterMode.Bilinear);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/TriLinear")]
        static void ChangeFilterModeTRilinear()
        {
            SelectedFilterMode(FilterMode.Trilinear);
        }

        public static void SelectedChangeIsReadable(bool enabled)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexChangeIsReadable(tex, enabled);
            }
        }

        public static void SetTexChangeIsReadable(Texture2D tex, bool enabled)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.isReadable = enabled;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeMaxTextureSize(int size)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexMaxTextureSize(tex, size);
            }
        }

        public static void SetTexMaxTextureSize(Texture2D tex, int size)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.maxTextureSize = size;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeNonPowerOf2(TextureImporterNPOTScale scale)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexNonPowerOf2(tex, scale);
            }
        }

        public static void SetTexNonPowerOf2(Texture2D tex, TextureImporterNPOTScale scale)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.npotScale = scale;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeMipMap(bool enabled)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexMipMap(tex, enabled);
            }
        }

        public static void SetTexMipMap(Texture2D tex, bool enabled)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.mipmapEnabled = enabled;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeTextureType(TextureImporterType type)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureType(tex,type);
            }
        }

        public static void SetTexTextureType(Texture2D tex, TextureImporterType type)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.textureType = type;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeTextureFormatSettings(TextureImporterCompression format)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureFormatSettings(tex, format);
            }
        }

        public static void SetTexTextureFormatSettings(Texture2D tex, TextureImporterCompression format)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.textureCompression = format;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedFilterMode(FilterMode type)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureFilterMod(tex, type);
            }
        }

        public static void SetTexTextureFilterMod(Texture2D tex, FilterMode format)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.filterMode = format;
            AssetDatabase.ImportAsset(path);
        }

        static Object[] GetSelectedTextures()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }
        #endregion

        #region Particle例子系统
        [MenuItem("CommonTools/Particle/修改粒子大小可缩放(选中物体)", false, 0)]
        static void SetPaticleScalingMode()
        {
            object[] objs = Selection.objects;
            for (int i = 0; i < objs.Length; i++)
            {
                GameObject obj = objs[i] as GameObject;
                ParticleSystem par1 = obj.transform.GetComponent<ParticleSystem>();
                if (par1 != null)
                {
                    par1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                }
                Transform[] trans = obj.transform.GetComponentsInChildren<Transform>();
                foreach (var tran in trans)
                {
                    ParticleSystem par = tran.GetComponent<ParticleSystem>();
                    if (par != null)
                    {
                        par.scalingMode = ParticleSystemScalingMode.Hierarchy;
                    }
                }
            }
            AssetDatabase.SaveAssets();
        }


        #endregion

        #region Mesh
        [MenuItem("CommonTools/Mesh/设置FBX模式(readwrite,compress)(选择文件夹)")]
        public static void SetFolderFbxType()
        {
            Object[] folds = Selection.objects;
            for (int iii = 0; iii < folds.Length; iii++)
            {
                string relatepath = AssetDatabase.GetAssetPath(folds[iii]);
                string folderpath = Path.GetFullPath(relatepath);
                GetAllFiles(folderpath.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
                for (int i = 0; i < files.Count; i++)
                {
                    if (!Path.GetExtension(files[i]).Equals(".FBX"))
                        continue;
                    ModelImporter assets = AssetImporter.GetAtPath(files[i]) as ModelImporter;
                    if (assets == null)
                    {
                        Debug.LogError("assetimport is null" + files[i]);
                        continue;
                    }
                    if (files[i].Contains("@"))
                    {
                        assets.isReadable = false;
                        assets.importMaterials = false;
                        assets.animationCompression = ModelImporterAnimationCompression.Optimal;
                    }
                    else
                    {
                        assets.importAnimation = false;
                        assets.isReadable = false;
                    }
                    assets.SaveAndReimport();
                }
            }
            AssetDatabase.Refresh();
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

        //存在当前文件夹下所有的对应后缀的子物体。
        static List<string> files = new List<string>(1000);

        public static void GetAllFiles(string path)
        {
            files.Clear();
            CollectAllFiles(path);
        }

        public static void CollectAllFiles(string path)
        {
            string[] localfiles = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            for (int i = 0; i < localfiles.Length; i++)
            {
                string filepath = localfiles[i];
                if (filepath.Contains(".meta")) continue;
                files.Add(filepath);
            }
            for (int j = 0; j < dirs.Length; j++)
            {
                CollectAllFiles(dirs[j]);
            }
        }

        /// <summary>
        ///压缩动画，数值的精度改为3位
        ///去掉scale曲线
        /// </summary>
        public static void CompressAnimationClip(GameObject g)
        {
            List<AnimationClip> animationClipList = new List<AnimationClip>(AnimationUtility.GetAnimationClips(g));
            if (animationClipList.Count == 0)
            {
                AnimationClip[] objectList = UnityEngine.Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
                animationClipList.AddRange(objectList);
            }

            foreach (AnimationClip theAnimation in animationClipList)
            {
                try
                {
                    if (!g.name.Contains("mon_3@show1"))
                    {
                        //去除scale曲线
                        foreach (EditorCurveBinding theCurveBinding in AnimationUtility.GetCurveBindings(theAnimation))
                        {
                            string name = theCurveBinding.propertyName.ToLower();
                            if (name.Contains("scale"))
                            {
                                AnimationUtility.SetEditorCurve(theAnimation, theCurveBinding, null);
                            }
                        }
                    }

                    //浮点数精度压缩到f3
                    AnimationClipCurveData[] curves = null;
#pragma warning disable CS0618 // 类型或成员已过时
                    curves = AnimationUtility.GetAllCurves(theAnimation);
#pragma warning restore CS0618 // 类型或成员已过时
                    Keyframe key;
                    Keyframe[] keyFrames;
                    for (int ii = 0; ii < curves.Length; ++ii)
                    {
                        AnimationClipCurveData curveDate = curves[ii];
                        if (curveDate.curve == null || curveDate.curve.keys == null)
                        {
                            continue;
                        }
                        keyFrames = curveDate.curve.keys;
                        for (int i = 0; i < keyFrames.Length; i++)
                        {
                            key = keyFrames[i];
                            key.value = float.Parse(key.value.ToString("f4"));
                            key.inTangent = float.Parse(key.inTangent.ToString("f4"));
                            key.outTangent = float.Parse(key.outTangent.ToString("f4"));
                            keyFrames[i] = key;
                        }
                        curveDate.curve.keys = keyFrames;
                        theAnimation.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(string.Format("CompressAnimationClip Failed !!! animationPath : {0} error: {1}", "  ", e));
                }
            }
        }
        #endregion
    }
}



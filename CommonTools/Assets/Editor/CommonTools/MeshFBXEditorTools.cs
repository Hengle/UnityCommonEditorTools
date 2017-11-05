using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace CommonTools
{
    public class MeshFBXEditorTools
    {
        [MenuItem("CommonTools/Mesh/设置FBX模式(readwrite,compress)(选择文件夹)")]
        public static void SetFolderFbxType()
        {
            Object[] folds = Selection.objects;
            for (int iii = 0; iii < folds.Length; iii++)
            {
                string relatepath = AssetDatabase.GetAssetPath(folds[iii]);
                string folderpath = Path.GetFullPath(relatepath);
                CommonEditorTools.GetAllFiles(folderpath.Replace("\\", "/").Replace(Application.dataPath, "Assets"));
                for (int i = 0; i < CommonEditorTools.files.Count; i++)
                {
                    if (!Path.GetExtension(CommonEditorTools.files[i]).Equals(".FBX"))
                        continue;
                    ModelImporter assets = AssetImporter.GetAtPath(CommonEditorTools.files[i]) as ModelImporter;
                    if (assets == null)
                    {
                        Debug.LogError("assetimport is null" + CommonEditorTools.files[i]);
                        continue;
                    }
                    if (CommonEditorTools.files[i].Contains("@"))
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
    }
}

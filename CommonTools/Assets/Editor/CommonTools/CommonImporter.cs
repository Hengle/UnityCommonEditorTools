using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CommonImporter : AssetPostprocessor
{
    /// <summary>
    /// 贴图导入之前
    /// </summary>
    void OnPreprocessTexture()
    {
        TextureImporter tImporter = assetImporter as TextureImporter;
        tImporter.isReadable = false;
    }
    /// <summary>
    /// 贴图倒入之后
    /// </summary>
    /// <param name="tex"></param>
    void OnPostprocessTexture(Texture2D texure)
    {

    }
    /// <summary>
    /// 模型导入前设置
    /// </summary>
    void OnPreprocessModel()
    {
        ModelImporter mImporter = assetImporter as ModelImporter;
        mImporter.isReadable = false;
        mImporter.importMaterials = false;
        if (mImporter.name.Contains("@"))
        {
            mImporter.importAnimation = true;
            mImporter.animationCompression = ModelImporterAnimationCompression.Optimal;
        }
    }
    /// <summary>
    /// 模型导入后设置
    /// </summary>
    /// <param name="g"></param>
    void OnPostprocessModel(GameObject g)
    {
    }

    /// <summary>
    /// 音效导入前
    /// </summary>
    void OnPreprocessAudio()
    {
        AudioImporter audio = this.assetImporter as AudioImporter;

    }
    /// <summary>
    /// 音效导入后
    /// </summary>
    /// <param name="clip"></param>
    void OnPostprocessAudio(AudioClip clip)
    {

    }


    //所有的资源的导入，删除，移动，都会调用此方法，注意，这个方法是static的  
    public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log("OnPostprocessAllAssets");
        foreach (string str in importedAsset)
        {
            Debug.Log("importedAsset = " + str);
        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("deletedAssets = " + str);
        }
        foreach (string str in movedAssets)
        {
            Debug.Log("movedAssets = " + str);
        }
        foreach (string str in movedFromAssetPaths)
        {
            Debug.Log("movedFromAssetPaths = " + str);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
///搜索目标文件夹下所有文件
/// </summary>
public class AssetsSearch
{
    public List<string> Assets = new List<string>();
    public string RootPath;
    public List<string> IncludeExtensions = new List<string>();

    public AssetsSearch(string _rootpath)
    {
        RootPath = _rootpath;
    }

    public void Search()
    {
        Assets.Clear();
        Recrusive(RootPath);
    }

    /// <summary>
    /// 递归遍历所有的资源
    /// </summary>
    private void Recrusive(string rootdir)
    {
        string[] dirs = Directory.GetDirectories(rootdir);
        string[] files = Directory.GetFiles(rootdir);
        foreach (var file in files)
        {
            string extention = Path.GetExtension(file).ToLower();
            if (IncludeExtensions.Contains(extention))
            {
                Assets.Add(file);
            }
        }
        foreach (var dir in dirs)
        {
            Recrusive(dir);
        }
    }
}

public class AnimationClipSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".anim" };
    public AnimationClipSearch(string _rootpath):base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class AudioClipSearch : AssetsSearch
{
    readonly string[] SelfExtension = { ".aif", ".wav", ".mp3", ".ogg" };
    public AudioClipSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class MaterialSearch : AssetsSearch
{
    readonly string[] SelfExtension = { ".mat" };
    public MaterialSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class MeshSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".fbx", ".dae", ".3ds", ".dxf", ".obj", ".skp" };
    public MeshSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class ModelSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".fbx", ".max", ".jas", ".c4d", ".mb", ".ma", ".lxo", ".dxf", ".blend", ".dae", ".3ds" };
    public ModelSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class PrefabSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".prefab" };
    public PrefabSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class SceneSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".unity" };
    public SceneSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class ShaderSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".shader" };
    public ShaderSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class TextureSearch : AssetsSearch
{
    readonly string[] SelfExtension = new string[] { ".psd", "tiff", "jpg", ".tga", ".png", ".gif", ".bmp", ".iff", ".pict" };
    public TextureSearch(string _rootpath) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}

public class AssetsSearchWithExtension : AssetsSearch
{
    public AssetsSearchWithExtension(string _rootpath, string[] SelfExtension) : base(_rootpath)
    {
        foreach (var extension in SelfExtension)
        {
            IncludeExtensions.Add(extension);
        }
    }
}
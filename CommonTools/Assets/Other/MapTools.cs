using System;
using UnityEngine;
using System.Collections;

public class MapTools : MonoBehaviour
{
    public Camera camera;
    public int width;
    public int height;
    public int _miniWidth;
    public int _miniHeight;
    public string picname;

    [ContextMenu("GetmapMini")]
    public void GetMiniInfo()
    {
        if (camera == null) camera = GetComponent<Camera>();
        transform.localPosition = new Vector3(width / 2f, width, height / 2f);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        camera.orthographic = true;
        camera.orthographicSize = width / 2f;
        CaptureMini(camera, new Rect(0, 0, _miniWidth, _miniHeight));
    }
    Texture2D CaptureMini(Camera camera, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 10);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        DestroyImmediate(rt);
        // 最后将这些纹理数据，成一个png图片文件  

        //垂直翻转

        //Texture2D newscreen = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        //int x_start = 0;
        //int y_start = 0;
        //for (int i = 0; i < rect.width; i++)
        //{
        //    for (int j = 0; j < rect.height; j++)
        //    {
        //        newscreen.SetPixel(i, j, screenShot.GetPixel(i, (int)rect.height - j));
        //    }
        //}
        //newscreen.Apply();

        byte[] bytes = screenShot.EncodeToJPG(70);
        string filename = Application.dataPath + "/" + picname + "_s.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张小照片: {0}", filename));

        return screenShot;
    }  


   
    [ContextMenu("GetmapPicture")]
    public void GetTerrainInfo()
    {
        if (camera == null) camera = GetComponent<Camera>();
        transform.localPosition = new Vector3(width / 2f, width, height/2f);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        camera.orthographic = true;
        camera.orthographicSize = width/2f;
        CaptureCamera(camera, new Rect(0, 0, width*32, height*32));

    }
    Texture2D CaptureCamera(Camera camera, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 10);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        DestroyImmediate(rt);
        // 最后将这些纹理数据，成一个png图片文件  

        //垂直翻转

        Texture2D newscreen = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        int x_start = 0;
        int y_start = 0;
        for (int i = 0; i < rect.width; i++)
        {
            for (int j = 0; j < rect.height; j++)
            {
                newscreen.SetPixel(i, j, screenShot.GetPixel(i, (int)rect.height - j));
            }
        }
        newscreen.Apply();

        byte[] bytes = newscreen.EncodeToJPG(20);
        string filename = Application.dataPath + "/" + picname + ".jpg";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张照片: {0}", filename));

        return newscreen;
    }  
}

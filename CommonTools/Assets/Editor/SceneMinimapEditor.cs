using UnityEngine;
using System.Collections;
using UnityEditor;

public class SceneMinimapEditor : MonoBehaviour {

    [MenuItem("GameObject/小地图生成", false, 0)]
    public static void MinimapTool()
    {
        GameObject cameraobj = new GameObject("camera");
        Camera cam = cameraobj.AddComponent<Camera>();
        cam.backgroundColor = new Color(0,0,0,1);
        MapTools maptool = cameraobj.AddComponent<MapTools>();
        maptool._miniWidth = 512;
        maptool._miniHeight = 512;
        maptool.width =100;
        maptool.height = 100;
        string scenename = "";
        for (int i = 0; i < 100; i++)
        {
            string temp = string.Format("{0}{1}","land",i.ToString());
            if (GameObject.Find(temp) != null)
            {
                scenename = temp;
                break;
            }
        }
        maptool.picname = scenename;
        maptool.GetMiniInfo();
    }
}

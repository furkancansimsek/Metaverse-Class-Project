using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSnap : MonoBehaviour
{
    public Camera cam1;
    int resWidth = 1280;
    int resHeight = 720;
    //[SerializeField] RenderTexture tvPanel;

    void Awake()
    {
        cam1 = GetComponent<Camera>();
    }

    public void Targets()
    {

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        cam1.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        cam1.Render();
        cam1.enabled = false;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        cam1.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = SnapName(resWidth,resHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));



    }
    public static string SnapName(int width, int height)
    {
        return string.Format("{0}/snap_{1}x{2}_{3}.png",
            Application.persistentDataPath,
            width,
            height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}


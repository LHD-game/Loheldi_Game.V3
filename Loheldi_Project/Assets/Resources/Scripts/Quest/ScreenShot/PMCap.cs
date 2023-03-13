using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PMCap : MonoBehaviour
{
    public Camera camera;       //보여지는 카메라.

    //private int resWidth;
    //private int resHeight;
    //string path;

    //public Image ScreenshotImg;
    public GameObject PlayerstatusImg;
    //public Image MyInfoImg;
    //public Material PlayerImage;
    public RenderTexture PlayerRenderTexture;
    //public Texture PlayerImageTexture;

    public GameObject PCamLight;
    void Start()
    {
        int Time;
        Time = int.Parse(DateTime.Now.ToString("HH"));
        if (Time < 4 || Time > 16) ;
        else if (SceneManager.GetActiveScene().name == "Game_Tooth")
            ;
        else
            PCamLight.GetComponent<Light>().intensity = 0.45f;
        
        //if (SceneManager.GetActiveScene().name == "Game_Tooth")
            //ScreenshotImg = GameObject.Find("PlayerImage").GetComponent<Image>(); //이미지 띄울 곳;
        //resWidth = 2400;
        //resHeight = 2400;
        //path = Application.dataPath + "/ScreenShot/";
        StartCoroutine(ClickScreenShot());


    }

    public IEnumerator ClickScreenShot()
    {
        yield return new WaitForEndOfFrame();
        //RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        //RenderTexture rt = Resources.Load<RenderTexture>("Resources/Textures/PlayerImage");
        camera.targetTexture = PlayerRenderTexture;
        //Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        //camera.Render();
        RenderTexture.active = PlayerRenderTexture;
        //screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        //screenShot.Apply();

        //ffbyte[] bytes = screenShot.EncodeToPNG();
        //File.WriteAllBytes(name, bytes);
        //Sprite sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
        //ScreenshotImg.sprite = sprite;

        //this.transform.position = this.transform.position + new Vector3(0, -1, 2);

        if (SceneManager.GetActiveScene().name == "MainField"|| SceneManager.GetActiveScene().name == "AcornVillage")
        {
            PlayerstatusImg.SetActive(true);
            //MyInfoImg.sprite = sprite;
            //PlayerImage.SetTexture("_MainTex", screenShot);
        }
        yield return new WaitForEndOfFrame();
        camera.gameObject.SetActive(false);
    }
}

#define img2
#define self2

using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.std_msgs;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageTransferSAVe : MonoBehaviour
{
    int ImageWidth = 648;
    int ImageHeight = 488;
    GameObject bottomCam;
    GameObject frontCam;
    RenderTexture bottomImage;
    RenderTexture frontImage;
    Texture2D imageToSend;
    Texture2D imageToSend2;
    bool firstSend = true;
    public bool Lock = false;
    GameObject obj;
    Ctrl_InputMsg msg;
    StringMsg imgMsg;
   
    #region panel variables
    public float frontCamThreshHigh_R = 175;
    public float frontCamThreshHigh_G = 175;
    public float frontCamThreshHigh_B = 175;
    public float frontCamThreshLow_R = 100;
    public float frontCamThreshLow_G = 100;
    public float frontCamThreshLow_B = 100;
    public float bottomCamThreshHigh_R = 175;
    public float bottomCamThreshHigh_G = 175;
    public float bottomCamThreshHigh_B = 175;
    public float bottomCamThreshLow_R = 100;
    public float bottomCamThreshLow_G = 100;
    public float bottomCamThreshLow_B = 100;

    public Text FRHigh;
    public Text FGHigh;
    public Text FBHigh;
    public Text FRLow;
    public Text FGLow;
    public Text FBLow;
    public Text BRHigh;
    public Text BGHigh;
    public Text BBHigh;
    public Text BRLow;
    public Text BGLow;
    public Text BBLow;
    #endregion


    // Use this for initialization
    void Start()
    {
      
        Time.fixedDeltaTime = 0.04f;
        obj = GameObject.Find("Main Camera");

        #region Texture Initializations
#if img2
        bottomImage = new RenderTexture(ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
        bottomImage.Create();

        bottomCam = GameObject.Find("BottomCam");
        bottomCam.GetComponent<Camera>().targetTexture = bottomImage;
        bottomCam.GetComponent<Camera>().Render();

        frontImage = new RenderTexture(ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
        frontImage.Create();

        frontCam = GameObject.Find("FrontCam");
        frontCam.GetComponent<Camera>().targetTexture = frontImage;
        frontCam.GetComponent<Camera>().Render();

        imageToSend = new Texture2D(bottomImage.width, bottomImage.height, TextureFormat.RGB24, false);
        imageToSend2 = new Texture2D(frontImage.width, frontImage.height, TextureFormat.RGB24, false);
#endif
#endregion
    }


    #region slider functions

    public void AdjustFCamThreshHigh_R(float i)
    {
        frontCamThreshHigh_R = (int)i;
        FRHigh.text = frontCamThreshHigh_R.ToString();
    }

    public void AdjustFCamThreshHigh_G(float i)
    {
        frontCamThreshHigh_G = (int)i;
        FGHigh.text = frontCamThreshHigh_G.ToString();
    }

    public void AdjustFCamThreshHigh_B(float i)
    {
        frontCamThreshHigh_B = (int)i;
        FBHigh.text = frontCamThreshHigh_B.ToString();
    }

    public void AdjustFCamThreshLow_R(float i)
    {
        frontCamThreshLow_R = (int)i;
        FRLow.text = frontCamThreshLow_R.ToString();
    }

    public void AdjustFCamThreshLow_G(float i)
    {
        frontCamThreshLow_G = (int)i;
        FGLow.text = frontCamThreshLow_G.ToString();
    }

    public void AdjustFCamThreshLow_B(float i)
    {
        frontCamThreshLow_B = (int)i;
        FBLow.text = frontCamThreshLow_B.ToString();
    }

    public void AdjustBCamThreshHigh_R(float i)
    {
        bottomCamThreshHigh_R = (int)i;
        BRHigh.text = bottomCamThreshHigh_R.ToString();
    }

    public void AdjustBCamThreshHigh_G(float i)
    {
        bottomCamThreshHigh_G = (int)i;
        BGHigh.text = bottomCamThreshHigh_G.ToString();
    }

    public void AdjustBCamThreshHigh_B(float i)
    {
        bottomCamThreshHigh_B = (int)i;
        BBHigh.text = bottomCamThreshHigh_B.ToString();
    }

    public void AdjustBCamThreshLow_R(float i)
    {
        bottomCamThreshLow_R = (int)i;
        BRLow.text = bottomCamThreshLow_R.ToString();
    }

    public void AdjustBCamThreshLow_G(float i)
    {
        bottomCamThreshLow_G = (int)i;
        BGLow.text = bottomCamThreshLow_G.ToString();
    }

    public void AdjustBCamThreshLow_B(float i)
    {
        bottomCamThreshLow_B = (int)i;
        BBLow.text = bottomCamThreshLow_B.ToString();
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        // Examine whether Lock is required
#if img2      
        //encoding part:
        StringBuilder pixelsToSend1 = new StringBuilder("", 500000);
        StringBuilder pixelsToSend2 = new StringBuilder("", 500000);
        StringBuilder pixelsToSend3 = new StringBuilder("", 500000);
        StringBuilder pixelsToSend4 = new StringBuilder("", 500000);
        StringBuilder pixelsToSend5 = new StringBuilder("", 500000);
        StringBuilder pixelsToSend6 = new StringBuilder("", 500000);

        RenderTexture.active = bottomImage;
        imageToSend.ReadPixels(new Rect(0, 0, bottomImage.width, bottomImage.height), 0, 0);
        imageToSend.Apply();

        Color32[] allPixels = imageToSend.GetPixels32();

        #region bottom camera optimization
        //Bottom Image R Component
        for (int i = 0; i < bottomImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < bottomImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * bottomImage.width + j];
                if ((currentVal.r > bottomCamThreshHigh_R && currentVal.g < bottomCamThreshLow_G && currentVal.b < bottomCamThreshLow_B) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend1.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend1.Append("-1");
            pixelsToSend1.Append(">");
        }

        //Bottom Image G Component
        for (int i = 0; i < bottomImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < bottomImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * bottomImage.width + j];
                if ((currentVal.g > bottomCamThreshHigh_G && currentVal.r < bottomCamThreshLow_R && currentVal.b < bottomCamThreshLow_B) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend2.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend2.Append("-1");
            pixelsToSend2.Append(">");
        }

        //Bottom Image B Component
        for (int i = 0; i < bottomImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < bottomImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * bottomImage.width + j];
                if ((currentVal.b > bottomCamThreshHigh_B && currentVal.r < bottomCamThreshLow_R && currentVal.g < bottomCamThreshLow_G) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend3.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend3.Append("-1");
            pixelsToSend3.Append(">");
        }
        #endregion

        RenderTexture.active = frontImage;
        imageToSend2.ReadPixels(new Rect(0, 0, frontImage.width, frontImage.height), 0, 0);
        imageToSend2.Apply();

        allPixels = imageToSend2.GetPixels32();

        #region front camera optimization
        //Front Image R Component
        for (int i = 0; i < frontImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < frontImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * frontImage.width + j];
                if ((currentVal.r > frontCamThreshHigh_R && currentVal.g < frontCamThreshLow_G && currentVal.b < frontCamThreshLow_B) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend4.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend4.Append("-1");
            pixelsToSend4.Append(">");
        }

        //Front Image G Component
        for (int i = 0; i < frontImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < frontImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * frontImage.width + j];
                if ((currentVal.g > frontCamThreshHigh_G && currentVal.r < frontCamThreshLow_R && currentVal.b < frontCamThreshLow_B) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend5.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend5.Append("-1");
            pixelsToSend5.Append(">");
        }
        //Front Image B Component
        for (int i = 0; i < frontImage.height; i++)
        {
            bool prev = false;
            bool noChanges = true;
            for (int j = 0; j < frontImage.width; j++)
            {
                Color32 currentVal = allPixels[(ImageHeight - 1 - i) * frontImage.width + j];
                if ((currentVal.b > frontCamThreshHigh_B && currentVal.r < frontCamThreshLow_R && currentVal.g < frontCamThreshLow_G) != prev)
                {
                    noChanges = false;
                    prev = !prev;
                    pixelsToSend6.Append(j).Append(":");
                }
            }
            if (noChanges)
                pixelsToSend6.Append("-1");
            pixelsToSend6.Append(">");
        }
        #endregion

        pixelsToSend1.Append("!").Append(pixelsToSend2).Append("!").Append(pixelsToSend3).Append("!")
            .Append(pixelsToSend4).Append("!").Append(pixelsToSend5).Append("!").Append(pixelsToSend6);

        //sending the image data
        try
        {
#if self2        
            imgMsg = new StringMsg(pixelsToSend1.ToString());
            obj.GetComponent<ROS_Initialize>().ros.Publish(ImagePublisher.GetMessageTopic(), imgMsg);
            Debug.Log("Sending to topic: " + ImagePublisher.GetMessageTopic());
            //Debug.Log(imgMsg);
#endif
        }
        catch (Exception e)
        {
            Debug.Log("Socket error" + e);
        }
#endif
    }

}


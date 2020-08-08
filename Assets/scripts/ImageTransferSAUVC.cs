#define img
#define self

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

public class ImageTransferSAUVC : MonoBehaviour
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
    StringMsg imgMsg;
    string sceneName;

    // Use this for initialization
    void Start()
    {   
        Time.fixedDeltaTime = 0.04f;
        obj = GameObject.Find("Main Camera");

        #region Texture Initializations
#if img
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
    
    // Update is called once per frame
    void Update()
    {
#if img
            //encoding part:
            StringBuilder imgToSend = new StringBuilder("", 500000);
        //bottom cam encoding
        RenderTexture.active = bottomImage;
            imageToSend.ReadPixels(new Rect(0, 0, bottomImage.width, bottomImage.height), 0, 0);
            imageToSend.Apply();
        Byte[] bottom_cam_image_jpg = ImageConversion.EncodeToJPG(imageToSend, 100);
        string bottom_cam_image_base64 = Convert.ToBase64String(bottom_cam_image_jpg);
        imgToSend.Append(bottom_cam_image_base64).Append("!");

        //front cam encoding
        RenderTexture.active = frontImage;
            imageToSend2.ReadPixels(new Rect(0, 0, frontImage.width, frontImage.height), 0, 0);
            imageToSend2.Apply();

        Byte[] front_cam_image_jpg = ImageConversion.EncodeToJPG(imageToSend2, 100);
        string front_cam_image_base64 = Convert.ToBase64String(front_cam_image_jpg);
        imgToSend.Append(front_cam_image_base64).Append("!");

        //sending the image data

        try
        {
#if self
                imgMsg = new StringMsg(imgToSend.ToString());
                obj.GetComponent<ROS_Initialize>().ros.Publish(ImagePublisher.GetMessageTopic(), imgMsg);
                //Debug.Log("Sending to topic: " + ImagePublisher.GetMessageTopic());
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


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

public class ImageTransferDepth : MonoBehaviour
{
    int ImageWidth = 648;
    int ImageHeight = 488;
    GameObject depthCam;
    GameObject rgbCam;
    RenderTexture depthImage;
    RenderTexture rgbImage;
    Texture2D imageToSend;
    Texture2D imageToSend2;
    GameObject obj;
    StringMsg imgMsg;
    // Start is called before the first frame update
        // Use this for initialization
    void Start()
    {   
        Time.fixedDeltaTime = 0.04f;
        obj = GameObject.Find("Main Camera");

        #region Texture Initializations
#if img
        depthImage = new RenderTexture(ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
        depthImage.Create();

        depthCam = GameObject.Find("Depth Camera");
        depthCam.GetComponent<Camera>().targetTexture = depthImage;
        depthCam.GetComponent<Camera>().Render();

        rgbImage = new RenderTexture(ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
        rgbImage.Create();

        rgbCam = GameObject.Find("RGB Camera");
        rgbCam.GetComponent<Camera>().targetTexture = rgbImage;
        rgbCam.GetComponent<Camera>().Render();

        imageToSend = new Texture2D(depthImage.width, depthImage.height, TextureFormat.RGB24, false);
        imageToSend2 = new Texture2D(rgbImage.width, rgbImage.height, TextureFormat.RGB24, false);
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
        RenderTexture.active = depthImage;
            imageToSend.ReadPixels(new Rect(0, 0, depthImage.width, depthImage.height), 0, 0);
            imageToSend.Apply();
        Byte[] depth_cam_image_jpg = ImageConversion.EncodeToJPG(imageToSend, 100);
        string depth_cam_image_base64 = Convert.ToBase64String(depth_cam_image_jpg);
        imgToSend.Append(depth_cam_image_base64).Append("!");

        //front cam encoding
        RenderTexture.active = rgbImage;
            imageToSend2.ReadPixels(new Rect(0, 0, rgbImage.width, rgbImage.height), 0, 0);
            imageToSend2.Apply();

        Byte[] rgb_cam_image_jpg = ImageConversion.EncodeToJPG(imageToSend2, 100);
        string rgb_cam_image_base64 = Convert.ToBase64String(rgb_cam_image_jpg);
        imgToSend.Append(rgb_cam_image_base64).Append("!");

        //sending the image data

        try
        {
#if self
                imgMsg = new StringMsg(imgToSend.ToString());
                obj.GetComponent<ROSInitializerSLAM>().rosSLAM.Publish(ImagePublisher.GetMessageTopic(), imgMsg);
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

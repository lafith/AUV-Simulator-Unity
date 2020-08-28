using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerSLAM : MonoBehaviour
{
    GameObject obj;
    void Start()
    {
        obj = GameObject.Find("Main Camera");
    }
    public void toMainMenu()
    {

        obj.GetComponent<ROSInitializerSLAM>().rosSLAM.Disconnect();
        Debug.Log("ROS-SLAM Disconnecting!");
        SceneManager.LoadScene("MainMenu");
    }
}

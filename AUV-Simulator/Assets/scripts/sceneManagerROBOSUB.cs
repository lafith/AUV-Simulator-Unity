using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerROBOSUB : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initPos,auvPos;
    GameObject[] drums;
    GameObject yellowFlare,obj,auv,bouy;
    void Start()
    {
        obj = GameObject.Find("Main Camera");
        //initial position of Main camera
        initPos = transform.position;
        auv = GameObject.FindWithTag("auv");
        auvPos=auv.transform.position;
        //fog:
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.196f, 0.717f, 0.43f, 1);
        RenderSettings.fogDensity = 0.05f;

        bouy=GameObject.Find("SlayVampires");
    }

    //Reset button which bring auv & the camera back to their initial position
    public void resetAUV()
    {
        //resetting camera
        transform.position = initPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        auv.GetComponent<Rigidbody>().velocity = Vector3.zero;
        auv.transform.rotation = Quaternion.Euler(0, 0, 0);
        auv.transform.position = auvPos;
    }

    public void changeFog(bool newValue)
    {
        if (newValue == true)
        {
            //RenderSettings.fog=true;
            RenderSettings.fogColor = new Color(0.196f, 0.717f, 0.43f, 1);
            RenderSettings.fogDensity = 0.05f;
        }
        else
        {
            RenderSettings.fogColor = new Color(0.68f, 0.76f, 0.77f, 1);
            RenderSettings.fogDensity = 0.06f;
        }
    }

    public void toMainMenu()
    {
        obj.GetComponent<ROSInitializerROBOSUB>().rosROBOSUB.Disconnect ();
        Debug.Log ("ROS-ROBOSUB Disconnecting!");
        SceneManager.LoadScene("MainMenu");

    }
    void Update()
    {
        bouy.transform.Rotate(Vector3.up, 2.5f*6f*Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerSAUVC : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initPos;
    GameObject[] drums;
    GameObject yellowFlare;
    GameObject obj;
    void Start()
    {
        obj = GameObject.Find("Main Camera");
        //initial position of Main camera
        initPos = transform.position;
        //randomizing the positions of drums
        drums = GameObject.FindGameObjectsWithTag("drum");
        for (int i = 0; i < drums.Length; i++)
        {
            GameObject obj = drums[i];
            int random_i = UnityEngine.Random.Range(0, i);
            drums[i] = drums[random_i];
            drums[random_i] = obj;
            Vector3 posi = drums[i].transform.position;
            drums[i].transform.position = drums[random_i].transform.position;
            drums[random_i].transform.position = posi;

        }
        //fog:
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.68f, 0.76f, 0.77f, 1);
        RenderSettings.fogDensity = 0.06f;
    }

    //Reset button which bring auv & the camera back to their initial position
    public void resetAUV()
    {
        //resetting camera
        transform.position = initPos;
        transform.rotation = Quaternion.Euler(58, 90, 0);
        //resetting auv
        GameObject auv = GameObject.FindWithTag("auv");
        auv.GetComponent<Rigidbody>().velocity = Vector3.zero;
        auv.transform.rotation = Quaternion.Euler(0, 90, 0);
        auv.transform.position = new Vector3(-12, 1, -11);
    }

    public void changeFog(bool newValue)
    {
        if (newValue == true)
        {
            //RenderSettings.fog=true;
            RenderSettings.fogColor = new Color(0.196f, 0.717f, 0.43f, 1);
            RenderSettings.fogDensity = 0.2f;
        }
        else
        {
            RenderSettings.fogColor = new Color(0.68f, 0.76f, 0.77f, 1);
            RenderSettings.fogDensity = 0.06f;
        }
    }

    public void toMainMenu()
    {
        obj.GetComponent<ROSInitializerSAUVC>().rosSAUVC.Disconnect ();
        Debug.Log ("ROS-SAUVC Disconnecting!");
        SceneManager.LoadScene("MainMenu");

    }
    void Update()
    {
        //blue: 174,194,197,255 - 0.06
        //green: 50,183,110,255 - 0.2

    }
}

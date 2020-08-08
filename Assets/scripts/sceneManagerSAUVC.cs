using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManagerSAUVC : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initPos;
    GameObject[] drums;
    GameObject yellowFlare;
    void Start()
    {
        //initial position of Main camera
        initPos=transform.position;
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

    }

    //Reset button which bring auv & the camera back to their initial position
    public void resetAUV(){
        //resetting camera
        transform.position=initPos;
        transform.rotation=Quaternion.Euler(58,90,0);
        //resetting auv
        GameObject auv=GameObject.FindWithTag("auv");
        auv.GetComponent<Rigidbody>().velocity=Vector3.zero;
        auv.transform.rotation=Quaternion.Euler(0,90,0);
        auv.transform.position=new Vector3(-12,1,-11);
    }
    void Update()
    {
   
    }
}

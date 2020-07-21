using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.std_msgs;

public class Pinger_Flare : MonoBehaviour {
    //pinger initializations
    public GameObject[] drums;
    public GameObject pinger;
    public GameObject auv;
    Float32Msg pingermsg;
    GameObject obj;

    //flare initializations
    public GameObject flare;
    //public GameObject auv;
    Float32Msg flaremsg;
    //GameObject obj;

    public GameObject gate;

    void Awake()
    {
        drums = GameObject.FindGameObjectsWithTag("Drum");
    }
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Main Camera");
    
        //randomizing gate position:
//        gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y, UnityEngine.Random.Range(-70,-8));
        //randomizing gate size:
        //gate.transform.lossyScale = new Vector3(3, 3, 3);
        //randomizing the positions of drums
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

        //placing the pinger in a random drum
        int random_drum = UnityEngine.Random.Range(0, drums.Length);
        pinger.transform.position = drums[random_drum].transform.position;
/*
        //placing the flare at a random position in a specific area :
        float rz = UnityEngine.Random.Range(-80, -7);
        if (rz < -27 && rz > -58)
        {
            rz = rz + 20;
        }

        float rx = UnityEngine.Random.Range(47, 80);
        Vector3 pos = new Vector3(rx, -7, rz);
        flare.transform.position = pos;
*/
    }

    float PingerAngle()
    {
        Vector3 a = auv.transform.right;
        a.y = 0;
        Vector3 p = pinger.transform.position - auv.transform.position;
        p.y = 0;
        float d = Vector3.Angle(a, p);
        if (Vector3.Cross(a, p).y > 0)
        {
            return d;
        }
        else
        {
            return -d;
        }
    }

    public float FlareAngle()
    {
        Vector3 a = auv.transform.right;
        a.y = 0;
        Vector3 f = flare.transform.position - auv.transform.position;
        f.y = 0;
        float d = Vector3.Angle(a, f);
        if (Vector3.Cross(a, f).y > 0)
        {
            return d;
        }
        else
        {
            return -d;
        }
    }
    // Update is called once per frame
    void Update () {
        try
        {
            float p = PingerAngle();
            pingermsg = new Float32Msg(p);
            obj.GetComponent<ROS_Initialize>().ros.Publish(PingerPublisher.GetMessageTopic(), pingermsg);
            //   Debug.Log("Sending: PingerAngle = " + p);
            //    Debug.Log("Sending to topic: " + PingerPublisher.GetMessageTopic());
                 

            float d = FlareAngle();
            flaremsg = new Float32Msg(d);
            obj.GetComponent<ROS_Initialize>().ros.Publish(FlarePublisher.GetMessageTopic(), flaremsg);

            //Debug.Log("Sending: FlareAngle = " + d);
            //Debug.Log("Sending to topic: " + FlarePublisher.GetMessageTopic());
             
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
}

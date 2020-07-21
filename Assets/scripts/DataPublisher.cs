#define notSelf
#define UsePics

#region Using
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
#endregion

public class DataPublisher : MonoBehaviour
{
    CombinedMsg msg;
    GameObject obj;
    public bool Lock = false;
    Vector3 prevVelocity = Vector3.zero;
    Vector3 prevRot;

    void Start () {
        Time.fixedDeltaTime = 0.04f;
		obj = GameObject.Find ("Main Camera");
  		prevRot = transform.parent.transform.rotation.eulerAngles;          
    }
 
    //run SendData() each frame:
	void FixedUpdate () {
        Vector3 CurRot = transform.parent.transform.rotation.eulerAngles;
			if (!Lock) {
				Lock = true;
				SendData ();
			}
	}

    /* Send sensor data as feedback to the control algorithm. The data sent includes the orientation of the vehicle,
	 * acceleration in all three dimensions, depth under water, and forward velocity in local frame.
	 * */

    void SendData() {
		try {
			Vector3 CurRot = transform.parent.transform.rotation.eulerAngles;

			if(CurRot.x > 180.0f)
				CurRot.x -= 360.0f;
			if(CurRot.y > 180.0f)
				CurRot.y -= 360.0f;
			if(CurRot.z > 180.0f)
				CurRot.z -= 360.0f;

            //CurRot *= (float)Math.PI / 180.0f;
			Vector3 curVelocity = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().velocity);
			Vector3 CurAcc = (curVelocity - prevVelocity)/Time.deltaTime;
			prevVelocity = curVelocity;

			Vector3 Omega = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().angularVelocity);
			prevRot = CurRot;

			float modifiedDepth = (-transform.parent.position.y*15.0f);//+930.0f;

            #region for old controller
            //Uncomment for old controller
            			float[] angular = new float[]{-CurRot.x, CurRot.z, CurRot.y};
             			float[] linear = new float[]{CurAcc.x, -CurAcc.z, -CurAcc.y};
                        float depth = modifiedDepth;

             			msg = new CombinedMsg(angular, linear,depth);
            #endregion

            #region for new controller
            //For new controller
         //   float[] velocity = new float[]{curVelocity.x, -curVelocity.z, -curVelocity.y};
			//float[] acceleration = new float[]{CurAcc.x, -CurAcc.z, -CurAcc.y};
			//float[] angle = new float[]{-CurRot.x, CurRot.z, CurRot.y};
			//float[] omega = new float[]{-Omega.x, Omega.z, Omega.y};

			//msg = new Ctrl_InputMsg(velocity, acceleration, angle, omega, modifiedDepth);
            #endregion
#if notSelf
			obj.GetComponent<ROS_Initialize> ().ros.Publish(ROSPublisher.GetMessageTopic(), msg);
			#endif
		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}

}

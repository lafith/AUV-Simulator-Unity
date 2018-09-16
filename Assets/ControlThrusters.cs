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

public class ControlThrusters : MonoBehaviour {

      #region Variable Initializations
    /*   int ImageWidth = 648;
       int ImageHeight = 488;
       GameObject bottomCam;
       GameObject frontCam;
       RenderTexture bottomImage;
       RenderTexture frontImage;
       Texture2D imageToSend;
       Texture2D imageToSend2;

      
       StringMsg imgMsg;
       #endregion


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
      
    */
    #endregion
      
 //  bool firstSend = true;

    Ctrl_InputMsg msg;
    GameObject obj;
    public bool Lock = false;
    Vector3 prevVelocity = Vector3.zero;
    Vector3 prevRot;

    void Start () {
        Time.fixedDeltaTime = 0.04f;
		obj = GameObject.Find ("Main Camera");
  		prevRot = transform.parent.transform.rotation.eulerAngles;          
    }

    /*   #region slider functions

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
           bottomCamThreshHigh_R= (int)i;
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
       */
  
    /* Applies the forces received from the control algorithm to the thrusters.
	 * */

    public void AddForces()
	{

		short[] ForceVals = ReadSocket.ForceVals;
		transform.GetChild (0).GetComponent<ThrusterControl> ().AddForce (ForceVals [0]);
		transform.GetChild (1).GetComponent<ThrusterControl> ().AddForce (ForceVals [1]);
		transform.GetChild (2).GetComponent<ThrusterControl> ().AddForce (ForceVals [2]);
		transform.GetChild (3).GetComponent<ThrusterControl> ().AddForce (ForceVals [3]);
		transform.GetChild (4).GetComponent<ThrusterControl> ().AddForce (ForceVals [4]);
		transform.GetChild (5).GetComponent<ThrusterControl> ().AddForce (ForceVals [5]);
	}

    //run SendData() each frame:
	void FixedUpdate () {
        Vector3 CurRot = transform.parent.transform.rotation.eulerAngles;
        AddForces ();
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

			CurRot *= (float)Math.PI/180.0f;
			Vector3 curVelocity = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().velocity);
			Vector3 CurAcc = (curVelocity - prevVelocity)/Time.deltaTime;
			prevVelocity = curVelocity;

			Vector3 Omega = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().angularVelocity);
			prevRot = CurRot;

			float modifiedDepth = (-transform.parent.position.y*15.0f)+930.0f;
          
           #region for old controller
            //Uncomment for old controller
            //			float[] angular = new float[]{-CurRot.x, CurRot.z, CurRot.y};
           // 			float[] linear = new float[]{CurAcc.x, -CurAcc.z, modifiedDepth};
            
           // 			msg = new CombinedMsg(angular, linear);
            #endregion
           
           //For new controller
            float[] velocity = new float[]{curVelocity.x, -curVelocity.z, -curVelocity.y};
			float[] acceleration = new float[]{CurAcc.x, -CurAcc.z, -CurAcc.y};
			float[] angle = new float[]{-CurRot.x, CurRot.z, CurRot.y};
			float[] omega = new float[]{-Omega.x, Omega.z, Omega.y};

			msg = new Ctrl_InputMsg(velocity, acceleration, angle, omega, modifiedDepth);
            
			#if notSelf
			Debug.Log ("Sending: Depth = " + modifiedDepth);
			Debug.Log ("Sending: Force = " + CurAcc.y * 15000);
			Debug.Log("Sending to topic: " + ROSPublisher.GetMessageTopic());
			obj.GetComponent<ROS_Initialize> ().ros.Publish(ROSPublisher.GetMessageTopic(), msg);
			#endif
		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}

}
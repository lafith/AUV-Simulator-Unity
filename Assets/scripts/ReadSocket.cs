using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;


public class ReadSocket : MonoBehaviour {

	String msg;
	public static short[] ForceVals = {1500, 1500, 1500, 1500, 1500, 1500};
	bool firstCall = true;
	void Start () {
		Debug.Log ("In Start");
		Time.fixedDeltaTime = 0.04f;
	}



	void Update()
	{
	}

	/* The FixedUpdate function is responsible for receiving data from the control algorithm, i.e.,
	 * the values of forces at the thrusters. Once the control algorithm connects as a client, this
	 * function is responsible for periodically receiving the data, processing it, and storing the 
	 * values in the global variable 'ForceVals'.
	 * */
	void FixedUpdate()
	{
		ForceVals = ROSSubscriber.ForceVals;
		gameObject.GetComponent<ControlThrusters> ().Lock = false;

	}

}
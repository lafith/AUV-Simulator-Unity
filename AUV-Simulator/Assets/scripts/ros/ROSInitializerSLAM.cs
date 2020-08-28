using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;

public class ROSInitializerSLAM : MonoBehaviour {

	public ROSBridgeWebSocketConnection rosSLAM = null;
	// Use this for initialization
	void Start () {
		rosSLAM = new ROSBridgeWebSocketConnection ("ws://127.0.0.1", 9090);
		//rosSLAM.AddSubscriber (typeof(ROSSubscriber));
		//rosSLAM.AddSubscriber(typeof(PropsIdSubscriber));
		rosSLAM.AddPublisher (typeof(ImagePublisher));
		//rosSLAM.AddPublisher (typeof(ROSPublisher));
		//rosSLAM.AddPublisher (typeof(PosPublisher));
		rosSLAM.Connect ();
		Debug.Log("ROS-SLAM Connected!!");
	}

	// Extremely important to disconnect from ROS. Otherwise packets continue to flow
	void OnApplicationQuit() {
		if(rosSLAM!=null) {
			Debug.Log ("ROS-SLAM Disconnecting!");
			rosSLAM.Disconnect ();
		}
	}

	// Update is called once per frame
	void Update () {
		rosSLAM.Render();
	}
}


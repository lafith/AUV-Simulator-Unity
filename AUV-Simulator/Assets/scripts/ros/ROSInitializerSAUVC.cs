using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;

public class ROSInitializerSAUVC : MonoBehaviour {

	public ROSBridgeWebSocketConnection rosSAUVC = null;
	// Use this for initialization
	void Start () {
		rosSAUVC = new ROSBridgeWebSocketConnection ("ws://127.0.0.1", 9090);
		rosSAUVC.AddSubscriber (typeof(ROSSubscriber));
		rosSAUVC.AddSubscriber(typeof(PropsIdSubscriber));
		rosSAUVC.AddPublisher (typeof(ImagePublisher));
		rosSAUVC.AddPublisher (typeof(ROSPublisher));
		rosSAUVC.AddPublisher (typeof(PosPublisher));
		rosSAUVC.Connect ();
		Debug.Log("ROS-SAUVC Connected!!");
	}

	// Extremely important to disconnect from ROS. Otherwise packets continue to flow
	void OnApplicationQuit() {
		if(rosSAUVC!=null) {
			Debug.Log ("ROS-SAUVC Disconnecting!");
			rosSAUVC.Disconnect ();
		}
	}

	// Update is called once per frame
	void Update () {
		rosSAUVC.Render();
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;

public class ROSInitializerROBOSUB : MonoBehaviour {

	public ROSBridgeWebSocketConnection rosROBOSUB = null;
	// Use this for initialization
	void Start () {
		rosROBOSUB = new ROSBridgeWebSocketConnection ("ws://127.0.0.1", 9090);
		rosROBOSUB.AddSubscriber (typeof(ROSSubscriber));
		rosROBOSUB.AddSubscriber(typeof(PropsIdSubscriber));
		rosROBOSUB.AddPublisher (typeof(ImagePublisher));
		rosROBOSUB.AddPublisher (typeof(ROSPublisher));
		rosROBOSUB.AddPublisher (typeof(PosPublisher));
		rosROBOSUB.Connect ();
		Debug.Log("ROS-ROBOSUB Connected!!");
	}

	// Extremely important to disconnect from ROS. Otherwise packets continue to flow
	void OnApplicationQuit() {
		if(rosROBOSUB!=null) {
			Debug.Log ("ROS-ROBOSUB Disconnecting!");
			rosROBOSUB.Disconnect ();
		}
	}

	// Update is called once per frame
	void Update () {
		rosROBOSUB.Render();
	}
}


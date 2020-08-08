using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using SimpleJSON;

public class PropsIdSubscriber : ROSBridgeSubscriber  {
    public static int id=0;
    static GameObject thrusters;
    public new static string GetMessageTopic() {
		return "/propId";
	}

	public new static string GetMessageType() {
		return "std_msgs/Int8";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new Int8Msg(msg);
	}

    public new static void CallBack(ROSBridgeMsg msg) {
        Int8Msg idMsg=(Int8Msg)msg;
        id=idMsg.GetData();
        thrusters=GameObject.Find("Thrusters");
        thrusters.GetComponent<DataPublisher>().SendPos(id);
    }
}
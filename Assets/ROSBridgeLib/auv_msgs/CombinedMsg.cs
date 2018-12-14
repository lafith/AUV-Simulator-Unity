using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace auv_msgs {
		public class CombinedMsg : ROSBridgeMsg {
			private float[] angular, linear;
            private float depth;

			public CombinedMsg(float[] _angular, float[] _linear,float _depth) {
				angular = _angular;
				linear = _linear;
                depth = _depth;
			}

			public CombinedMsg(JSONNode msg)
			{
				angular = new float[msg["angular"].Count];
				for (int i = 0; i < angular.Length; i++) {
					angular[i] = float.Parse(msg["angular"][i]);
				}

				linear = new float[msg["linear"].Count];
				for (int i = 0; i < linear.Length; i++) {
					linear[i] = float.Parse(msg["linear"][i]);
				}
                depth = float.Parse(msg["depth"]);
            }

			public static string getMessageType() {
				return "synchronizer/Combined";
			}

			public float[] GetAngular() {
				return angular;
			}

			public float[] GetLinear() {
				return linear;
			}

            public float GetDepth()
            {
                return depth;
            }
            public override string ToString ()
			{
				string array = "[";
				for (int i = 0; i < angular.Length; i++) {
					array = array + angular[i];
					if (angular.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < linear.Length; i++) {
					array2 = array2 + linear[i];
					if (linear.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "CombinedMsg [angular=" + array + ", linear=" + array2 + ", depth=" + depth + "]";
			}

			public override string ToYAMLString() {
				string array = "[";
				for (int i = 0; i < angular.Length; i++) {
					array = array + angular[i];
					if (angular.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < linear.Length; i++) {
					array2 = array2 + linear[i];
					if (linear.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "{\"angular\" : " + array + ", \"linear\" : " + array2 +", \"depth\" : " + depth  + "}";
			}

		}
	}
}

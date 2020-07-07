using UnityEngine;
using System.Collections;
using System;

public class ThrusterControl : MonoBehaviour {

	public Vector3 dir;
	public int thrusterNumber;
	Vector3 posForce;
	GameObject water;
	double a=-51.348085781063;
	double b=0.10072267395657193;
	double c=-0.00006784574094879734;
  	double d= 1.5694002475642106e-8;
	/* PWM to force, as per BlueRobotics T100 thruster.
	 * */
	double adjustForces(short initial) {
		double adjusted = 0.0f;
		/*
		if (initial >= 1470 && initial <= 1530) {
			adjusted = 0.0f;
//			Debug.Log ("Adjusted = " + adjusted + " for thruster " + thrusterNumber);
		}
		else if (initial > 1530) {
			initial -= 1530;
			adjusted = initial / 370.0f * 2.36f;
		} else {
			initial -= 1470;
			adjusted = initial / 370.0f * 1.85f;
		}
		adjusted *= 9.8f * 1000.0f;*/
//		Debug.Log ("Adjusted before return = " + adjusted + " for thruster " + thrusterNumber);
		if(initial>=1470.0 && initial <= 1530.0){
            adjusted=0.0;
        }
		else{
			adjusted=a+(b*initial)+(c*Math.Pow(initial,2.0))+(d*Math.Pow(initial,3.0)) ;
			adjusted = adjusted * 4.44822;
		}

		return adjusted;
	}

	void Start() {
		water = GameObject.Find ("Water Surface");
		posForce = transform.localPosition;
	}

	/* A public function which can be called from another script with the
	 * value of the force to be applied as argument. Applies the force to
	 * the thruster to which this script is attached, in the direction as
	 * specified in the global variable, for that thruster.
	 * */
	public void AddForce (short ForceMag) {
		
		double finalForce = adjustForces (ForceMag);
		Debug.Log ("Force " + finalForce + " being applied to thruster " + thrusterNumber);

		if(gameObject.transform.position.y < water.transform.position.y)
		{
			
			transform.GetComponent<Rigidbody> ().AddRelativeForce (
				dir * (float)finalForce, ForceMode.Force);

		}
		
}
}
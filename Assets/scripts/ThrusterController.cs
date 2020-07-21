using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThrusterController : MonoBehaviour
{
    	double a= -1*51.348085781063,
        b=0.10072267395657193,
        c= -1*0.00006784574094879734,
        d= 1.5694002475642106e-8;
        static Vector3[] dirs=new[] {new Vector3(0,-1,0),
                                     new Vector3(0,-1,0),
                                     new Vector3(0,-1,0),
                                     new Vector3(1,0,0),
                                     new Vector3(1,0,0),
                                     new Vector3(0,0,-1)};
        GameObject water;
        public double thrusterFactor=16.0f;
    void Start()
    {
        water = GameObject.Find ("WaterPro_DayTime");
    }

    public void AddForces(short[] ForceVals)
	{
		//Debug.Log ("ForceVals = " + ForceVals[0] + " " + ForceVals[1] + " " + ForceVals[2] + " " + ForceVals[3] + " " + ForceVals[4] + " " + ForceVals[5] + " " );
        AddForce(transform.GetChild(0).gameObject,ForceVals[0],dirs[0]);
        AddForce(transform.GetChild(1).gameObject,ForceVals[1],dirs[1]);
        AddForce(transform.GetChild(2).gameObject,ForceVals[2],dirs[2]);
        AddForce(transform.GetChild(3).gameObject,ForceVals[3],dirs[3]);
        AddForce(transform.GetChild(4).gameObject,ForceVals[4],dirs[4]);
        AddForce(transform.GetChild(5).gameObject,ForceVals[5],dirs[5]);
	}
    public void AddForce (GameObject thruster,short ForceMag,Vector3 dir) {
		double finalForce = adjustForces (ForceMag);//*thrusterFactor;
        Debug.Log("thruster : "+finalForce);
		if(thruster.transform.position.y < water.transform.position.y)
		{
			
			thruster.GetComponent<Rigidbody> ().AddRelativeForce (
				dir * (float)finalForce, ForceMode.Force);

		}
		
    }
    
    double adjustForces(short initial) {
        /*
        double adjusted = 0.0f;
        if(initial>=1470.0 && initial <= 1530.0){
            adjusted=0.0;
        }
        else{
            adjusted=a+(b*initial)+(c*Math.Pow(initial,2.0))+(d*Math.Pow(initial,3.0)) ;
            adjusted = adjusted * 4.44822;
        }*/
        
        	///*
            double adjusted = 0.0f;
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
		adjusted *= 9.8f * 1000.0f;
        //*/
        return adjusted;
    }

    
}

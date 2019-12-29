using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	public float waterLevel;
   // public Rigidbody thruster;
//	public float bounceDamp;
	Vector3 dims;
	float l, b, h;
    //float v;
	public Vector3 CenterOfMass = Vector3.zero;
	public float rho = 1f;
	Vector3 upthrust;
	GameObject COB_Pos;
    // public Rigidbody hull;
    //public Rigidbody frame;
   // public float v = Mathf.Pow(2.52f, 3f);
    void Start()
	{
       
		dims = GetComponent<Transform> ().localScale;
		Debug.Log (dims);
        //v = Mathf.Pow(10f,3f);
        l = dims.x;
       h = dims.y;
        b = dims.z;
        //h = hull.transform.localScale.y;
       

        GetComponent<Rigidbody> ().centerOfMass = CenterOfMass;
        //COB_Pos = GameObject.Find("Center of Buoyancy");
        //GetComponent<Rigidbody>().centerOfMass. = COB_Pos.transform.position;


    }

    void FixedUpdate () {
		float y = transform.position.y;
        //float y = hull.transform.position.y;
        //Debug.Log(y.ToString());
        // Debug.Log((waterLevel - y).ToString());
        //thruster.AddForce(100, 0, 0);

        Vector3 MaxUpthrust =l*h*b * rho * Physics.gravity * -1000f;
		if (waterLevel - y > h/2)
			upthrust = MaxUpthrust;
		else if (y - waterLevel > h/2)
			upthrust = Vector3.zero;
		else
			upthrust = MaxUpthrust / 2 + (waterLevel - y) * MaxUpthrust / h;
        Debug.Log(upthrust.ToString());
        //		Vector3 CenterOfBuoyancy = COB_Pos.transform.position;
        //		Debug.Log (CenterOfBuoyancy);
        //		COB_Pos.transform.GetComponent<Rigidbody>().AddForceAtPosition(upthrust, Vector3.zero);

      //  Vector3 position =new  Vector3(frame.transform.position.x, hull.transform.position.y, frame.transform.position.z);
            transform.GetComponent<Rigidbody>().AddForceAtPosition(upthrust,transform.position);
        //		Debug.Log ("Applying buoyancy at " + transform.position);
        //hull.AddForceAtPosition(upthrust, COB_Pos.transform.position);
        //Debug.Log(dims.x.ToString());
    }
}

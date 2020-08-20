using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	public float waterLevel;
 	Vector3 dims;
	float l, b, h;
    //float v;
	public Vector3 CenterOfMass = Vector3.zero;
	public float rho = 1f;
	Vector3 upthrust;
	GameObject COB_Pos;
	
    void Start()
	{
		
		dims = GetComponent<Transform> ().localScale;
		//Debug.Log (dims);
        l = dims.x;
        h = dims.y;
        b = dims.z;

        GetComponent<Rigidbody> ().centerOfMass = CenterOfMass;
    }

    void FixedUpdate () {
		float y = transform.position.y;
        Vector3 MaxUpthrust =(l*h*b) * rho * Physics.gravity * -500f;
		if (waterLevel - y > h/2)
			upthrust = MaxUpthrust;
		else if (y - waterLevel > h/2)
			upthrust = Vector3.zero;
		else
			upthrust = MaxUpthrust / 2 + (waterLevel - y) * MaxUpthrust / h;
        transform.GetComponent<Rigidbody>().AddForceAtPosition(upthrust,transform.position);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterEffects : MonoBehaviour {

    Color normalcolor;
    Color underwatercolor;
    public GameObject watersurface;
    public GameObject maincamera;
    
	// Use this for initialization
	void Start () {
        normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwatercolor = new Color(0, 0, 255, 15);
;	}
	
	// Update is called once per frame
	void Update () {
		if(maincamera.transform.position.y < watersurface.transform.position.y)
        {
            RenderSettings.fogColor = underwatercolor;
            RenderSettings.fogDensity = 0.002f;
        }
     

	}
}

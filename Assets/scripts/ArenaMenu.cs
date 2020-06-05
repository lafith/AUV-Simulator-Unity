using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaMenu : MonoBehaviour {
    
    public Rigidbody rb;
    public Vector3 v;
    public Vector3 r;
    public Vector3 q;
    public Vector3 w;
    public GameObject starting_zone;
    public GameObject qualifier;
    public GameObject cam;
    public Text xaxis;
    //stores AUV's initial position 
    public void Start()
    {
        v = starting_zone.transform.position;
        r = rb.transform.localEulerAngles;
        q = qualifier.transform.position;
        w = rb.transform.position - cam.transform.position;
        
    }

    public void Update()
    {
        xaxis.text = "   POSITION   ANGLE"+"\n"+"X    " + rb.transform.position.x.ToString("F2") + "          " + rb.transform.localEulerAngles.x.ToString("F2")+"\n"+ "Y    " + rb.transform.position.y.ToString("F2") + "          " + rb.transform.localEulerAngles.y.ToString("F2")+"\n"+"Z    " + rb.transform.position.z.ToString("F2") + "         " + rb.transform.localEulerAngles.z.ToString("F2");
    }
    //method to launch the main menu
    public void main_menu()
    {
        SceneManager.LoadScene("menu");
    }

    //method to reset the transforms of the AUV
    public void starting_zone_button()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.transform.position = v;
        rb.transform.localEulerAngles = r;
        cam.transform.position = v - w;
    }
   public void qualifier_button()
    {

        rb.velocity = new Vector3(0, 0, 0);
        rb.transform.position = q+new Vector3(0,0,-2);
        rb.transform.localEulerAngles = r+new Vector3(0,90,0);
        cam.transform.position = q - w;
    }
}

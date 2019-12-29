using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobosubArenaMenu : MonoBehaviour {
    public Rigidbody rb;
    public Rigidbody torpedo;
    public Vector3 semi_pos;
    public Vector3 init_rot;
    public Vector3 final_pos;
    public Vector3 cam_pos;
    public Vector3 torpedo_pos;
    public Vector3 cam_rot;
    //public GameObject semifinal_dock;
    //public GameObject final_dock;
    public GameObject cam;
    public Text xaxis;
    public GameObject bouy_1;
    public GameObject bouy_2;
    public GameObject pre_qual_dock;
    // Use this for initialization
    void Start () {
        semi_pos = rb.transform.position;
        init_rot = rb.transform.localEulerAngles;
        //final_pos = final_dock.transform.position;
        //semi_pos = semifinal_dock.transform.position;
        cam_pos = semi_pos- cam.transform.position;
        torpedo_pos = semi_pos - torpedo.transform.position;
        cam_rot = cam.transform.localEulerAngles;
	}

    public void semifinal()
    {
        rb.velocity = new Vector3(0, 0, 0);
        torpedo.velocity = new Vector3(0, 0, 0);
        rb.transform.position = semi_pos;
        cam.transform.position = semi_pos - cam_pos;
        torpedo.transform.position = semi_pos - torpedo_pos;
        rb.transform.localEulerAngles = init_rot;
        cam.transform.localEulerAngles = cam_rot;
    }
    public void final()
    {
        rb.velocity = new Vector3(0, 0, 0);
        torpedo.velocity = new Vector3(0, 0, 0);
        rb.transform.position = semi_pos+new Vector3(40,0,0);
        cam.transform.position = rb.transform.position - cam_pos;
        torpedo.transform.position = rb.transform.position - torpedo_pos;
        rb.transform.localEulerAngles = init_rot;
        cam.transform.localEulerAngles = cam_rot;
    }

    public void preQual()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.transform.position = pre_qual_dock.transform.position + new Vector3(8, 0, 0);
        cam.transform.position = rb.transform.position - new Vector3(5,-2,0);
        rb.transform.localEulerAngles = init_rot+new Vector3(0,90,0);
        cam.transform.localEulerAngles = cam_rot+new Vector3(0,90,0);
    }
    public void main_menu()
    {
        SceneManager.LoadScene("menu");
    }
    // Update is called once per frame
    public void Update () {
        xaxis.text = "   POSITION   ANGLE" + "\n" + "X    " + rb.transform.position.x.ToString("F2") + "          " + rb.transform.localEulerAngles.x.ToString("F2") + "\n" + "Y    " + rb.transform.position.y.ToString("F2") + "          " + rb.transform.localEulerAngles.y.ToString("F2") + "\n" + "Z    " + rb.transform.position.z.ToString("F2") + "         " + rb.transform.localEulerAngles.z.ToString("F2");
        bouy_1.transform.Rotate(Vector3.up * 6 * Time.deltaTime);
        bouy_2.transform.Rotate(Vector3.up * 6 * Time.deltaTime);

    }
}

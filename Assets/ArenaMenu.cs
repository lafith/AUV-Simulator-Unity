using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaMenu : MonoBehaviour {
    
    public Rigidbody rb;
    public Vector3 v;
    public Vector3 r;
    public Vector2 fcp;
    public Vector2 bcp;
    public Dropdown dp;
    public GameObject FCamPanel;
    public GameObject BCamPanel;
    //public Text title;
    public Text xaxis;
    //public Text yaxis;
    //public Text zaxis;
    List<string> ls = new List<string>() { "AdjustThreshold", "FrontCam", "BottomCam" };
    //stores AUV's initial position 
    public void Start()
    {

        fcp = FCamPanel.transform.position;
        bcp = BCamPanel.transform.position;
        v = rb.transform.position;
        r = rb.transform.localEulerAngles;
        Populate();
        //title.text = "   POSITION   ANGLE";
        
    }

    public void Update()
    {
        xaxis.text = "   POSITION   ANGLE"+"\n"+"X    " + rb.transform.position.x.ToString("F2") + "          " + rb.transform.localEulerAngles.x.ToString("F2")+"\n"+ "Y    " + rb.transform.position.y.ToString("F2") + "          " + rb.transform.localEulerAngles.y.ToString("F2")+"\n"+"Z    " + rb.transform.position.z.ToString("F2") + "         " + rb.transform.localEulerAngles.z.ToString("F2");
        //yaxis.text ="Y    " + rb.transform.position.y.ToString("F2") + "        " + rb.transform.localEulerAngles.y.ToString("F2");
        //zaxis.text = "Z    " + rb.transform.position.z.ToString("F2") + "        " + rb.transform.localEulerAngles.z.ToString("F2");
    }
    public void Populate()
    {
        dp.AddOptions(ls);
    }
    //method to launch the main menu
    public void main_menu()
    {
        SceneManager.LoadScene("menu");
    }

    //method to reset the transforms of the AUV
    public void reset_button()
    {

        rb.transform.position = v;
        rb.transform.localEulerAngles = r;
        rb.velocity = new Vector3(0, 0, 0);
    }

   public void AdjustPanel(int j)
    {
        if (j == 1)
        {
            FCamPanel.transform.position = new Vector2(1175, 300);
            
        }

        if (j != 1)
        {
            FCamPanel.transform.position = fcp;
           
        }

        if (j == 2)
        {
            BCamPanel.transform.position = new Vector2(1175, 300);
            
        }
        if (j != 2)
        {
            BCamPanel.transform.position = bcp;
        }

       
    }
   
}

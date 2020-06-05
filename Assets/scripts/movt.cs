using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib.auv_msgs;
public class movt : MonoBehaviour
{

    public Rigidbody rb;
    //VelocityMsg msg;
    //GameObject obj;

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.I))
        {
            rb.AddRelativeForce(0, 0, 900000 * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.K))
        {
            rb.AddRelativeForce(0, 0, -900000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.J))
        {
            rb.AddRelativeForce(-900000 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            rb.AddRelativeForce(900000 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.N))
        {
            rb.AddRelativeForce(0,8000000 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.M))
        {
            rb.AddRelativeForce(0, -8000000 * Time.deltaTime, 0);
        }
        if (Input.GetKey("u"))
        {
            rb.AddRelativeTorque(0, 90000f * Time.deltaTime, 0);
        }
        else if (Input.GetKey("o"))
        {
            rb.AddRelativeTorque(0, -90000f * Time.deltaTime, 0);
        }
        
    }
    
}

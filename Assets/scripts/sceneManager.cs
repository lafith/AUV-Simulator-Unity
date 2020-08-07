using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefabAuv;
    Vector3 initPos;
    GameObject auv;
    bool b=false;
    void Start()
    {
        auv=Instantiate(prefabAuv) as GameObject;
        auv.transform.position=new Vector3(-12,1,-11);
        initPos=transform.position;
    }

    // Update is called once per frame
    public void resetAUV(){
        transform.position=initPos;
        transform.rotation=Quaternion.Euler(58,90,0);
        
        GameObject auv=GameObject.FindWithTag("auv");
        auv.GetComponent<Rigidbody>().velocity=Vector3.zero;
        auv.transform.rotation=Quaternion.Euler(0,90,0);
        auv.transform.position=new Vector3(-12,1,-11);
    }
    void Update()
    {
   
    }
}

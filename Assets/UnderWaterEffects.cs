/*
Created based on the following article:
https://medium.com/@mukulkhanna/creating-basic-underwater-effects-in-unity-9a9400bde928
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderWaterEffects : MonoBehaviour {
    public float waterHeight;
    private bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
    //public float density=0.01f;
    // Use this for initialization
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.70f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y < waterHeight) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;

    }

    void SetUnderwater()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.018f;

    }
    /*
    public void setDensity(float newDensity)
    {
        density = newDensity;
    }*/
}

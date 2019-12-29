/*
Created based on the following articles:
https://medium.com/@mukulkhanna/creating-basic-underwater-effects-in-unity-9a9400bde928
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterCaustics : MonoBehaviour {

    public float fps = 30.0f;         //footage fps
    public Texture2D[] frames;      //caustics images

    private int frameIndex;
    private Projector projector;    //Projector GameObject

    void Start()
    {
        projector = GetComponent<Projector>();
        NextFrame();
        InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    void NextFrame()
    {
        projector.material.SetTexture("_ShadowTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }
}

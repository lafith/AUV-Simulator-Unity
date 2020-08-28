using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void toSauvc(){
        SceneManager.LoadScene("sauvc");
    }

    public void toSLAMtest(){
        SceneManager.LoadScene("SLAMtest");
    }

    public void toROBOSUB(){
        SceneManager.LoadScene("robosub");
    }
}

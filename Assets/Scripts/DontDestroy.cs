using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.Find("Scoreboard") != null)
        {
            GameObject.Find("DontDestroy");
            
        }
        else
        {
            GameObject.Find("Scoreboard").transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }


    }
}

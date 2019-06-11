using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int level)
    { 
    	// print("LOADING " + level);
    	Application.LoadLevel(level);
    	Time.timeScale = 1;
    }

    public void doExitGame()
    {
    	Application.Quit();
 	}
}

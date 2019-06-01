using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int level)
    { 
    	print("LOADING " + level);
    	Application.LoadLevel(level);
    }

    public void doExitGame()
    {
    	Application.Quit();
 	}
}

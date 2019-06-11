using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
	private GameController gc;
	public GameObject PauseMenu;

	public void Pause () {
		if (!gc.isGameOver ()){
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				gc.isPaused = true;
				PauseMenu.SetActive (true);
			}
			else {
				Time.timeScale = 1;
				gc.isPaused = false;
				PauseMenu.SetActive (false);
			}
		}
	}

	void Start () {
		GameObject go = GameObject.FindWithTag("GameController");
        if (go) 
            gc = go.GetComponent<GameController>();
        // else
        //     Debug.Log ("cannot find 'GameController' script");
	}
}

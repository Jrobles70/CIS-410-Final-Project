using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed;
	private GameController gc;

	public void SetVelocity (float speed) {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

    void Start()
    {
    	GameObject go = GameObject.FindWithTag("GameController");
        if (go) 
            gc = go.GetComponent<GameController>();
        // else
        //     Debug.Log ("cannot find 'GameController' script");
    	SetVelocity (speed * gc.speedFactor);
    }
}

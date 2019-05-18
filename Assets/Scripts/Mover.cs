using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed;
	public void SetVelocity (float speed) {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

    void Start()
    {
    	SetVelocity (speed);
    }
}

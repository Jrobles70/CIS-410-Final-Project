using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public float speed = 400.0f;
    private Rigidbody rb;

    public void SetVelocity (Vector3 velocity) {
		rb = GetComponent<Rigidbody>();
		rb.velocity =  velocity * -1;
    }

    void Start () {
    	SetVelocity (new Vector3 (0.0f, 0.0f, speed));
		// Debug.Log("started moving at velocity: " + speed);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;
    private bool collided;
    public float newZ;
    public GameObject newRoad;
    private Quaternion rot;

    public void SetVelocity (Vector3 velocity) {
		rb = GetComponent<Rigidbody>();
		rb.velocity =  velocity * -1;
    }

    void Start () {
    	rot = new Quaternion (0, 0, 0, 1);
    	SetVelocity (new Vector3 (0.0f, 0.0f, speed));
		collided = false;
		// Debug.Log("started moving at velocity: " + speed);
	}

	void OnTriggerExit (Collider other) {
		// Debug.Log (gameObject.name+ " collided with " + other.name);
		if (other.tag == "Player" && !collided) {
			Vector3 pos = new Vector3 (0.0f, 0.0f, newZ);
			Instantiate (newRoad, pos, rot);
			collided = true;
		}
	}
}

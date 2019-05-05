using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public int speed = 1;
    private Rigidbody rb;
    private bool collided;
    public float pos_z;
    public GameObject newRoad;

    void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed * -1;
		Debug.Log("started moving at velocity: " + speed);
		collided = false;
	}

	// void Update () {
		// if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
		// 	rb.velocity = transform.forward * speed * -2;
		// rb.velocity = transform.forward * speed * -1;
	// }

	void OnTriggerExit (Collider other) {
		Debug.Log (gameObject.tag + " collided with " + other.tag);
		if (other.tag == "Player" && !collided) {
			Vector3 pos = new Vector3 (0.0f, 0.0f, pos_z);
			Quaternion rot = new Quaternion (0, 0, 0, 1);
			Instantiate (newRoad, pos, rot);
			collided = true;
		}
	}
}

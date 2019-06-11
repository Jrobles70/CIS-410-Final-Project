using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
	public float z_pos;
	public float initial_z = 575.0f;
	public GameObject new_road;
	private Quaternion quat;
	public int batch_size = 2;
	private int incrementer = 1;
	private bool generate = true;

    void Start () {
    	quat = new Quaternion (0, 0, 0, 1);
    }

    void GenRoad () {
    	for (int i = 1; i < batch_size; i++) {
    		float new_z = (z_pos * (float)i) + initial_z;
    		Vector3 new_pos = new Vector3 (0.0f, 0.0f, new_z);
			Instantiate (new_road, new_pos, quat);
    	}
    	generate = false;
    }

	void OnTriggerEnter (Collider other) {
		if (other.tag == "GenerateRoad") {
			// Debug.Log (gameObject.name+ " collided with " + other.name);
			if (generate) {
				GenRoad ();
			}
			if (incrementer < batch_size) {
				incrementer ++;
			}
			if (incrementer == batch_size) {
				incrementer = 1;
				generate = true;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject newRoad = null;
	void Start () {
		Debug.Log (gameObject.tag + " started");
	}

	void OnTriggerExit (Collider other) {
		Debug.Log (gameObject.tag + " collided with " + other.tag);
		if (gameObject.tag == "Player" && other.tag == "GenerateRoad") {
			if (newRoad != null)
				return;

			Vector3 pos = new Vector3 (0.0f, 0.0f, 325.0f);
			Quaternion rot = new Quaternion (0, 0, 0, 1);
			Instantiate (newRoad, pos, rot);
			// Destroy (other.gameObject);
		}
	}
}

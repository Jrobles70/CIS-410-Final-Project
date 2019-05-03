using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBounds : MonoBehaviour
{
	void OnTriggerExit (Collider other) {
		Destroy (other.gameObject);
		Debug.Log ("Destroyed " + other.name);
	}
}

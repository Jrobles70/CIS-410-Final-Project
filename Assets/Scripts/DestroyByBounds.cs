using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBounds : MonoBehaviour
{
	void OnTriggerExit (Collider other) {
		Destroy (other.gameObject);
		//Debug.Log (other.name + "destroyed by bounds");
	}
}

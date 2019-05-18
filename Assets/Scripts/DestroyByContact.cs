using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	private GameController gc;
	public GameObject explosion;
	public Transform transform;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			Vector3 offset = new Vector3 (-5.0f, 7.5f, 0.0f);
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position + offset , transform.rotation);
			gc.GameOver();
			// Debug.Log (gameObject.tag + " destroyed by contact with " + other.tag);
		}
	}

	void Start () {
		GameObject go = GameObject.FindWithTag("GameController");
		if (go) 
			gc = go.GetComponent<GameController>();
		else
			Debug.Log ("cannot find 'GameController' script");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	private GameController gc;
	public GameObject explosion;
	public Transform transform;
	public bool shield = false;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			if (!shield) {
				Vector3 offset = new Vector3 (-5.0f, 6.7f, 4.0f);
				Destroy (other.gameObject);
				Destroy (gameObject);
				Quaternion rot = transform.rotation;
				rot.y = -90;
				Instantiate (explosion, transform.position + offset , rot);
				gc.GameOver();
				Debug.Log (gameObject.tag + " destroyed by contact with " + other.tag);
			}
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

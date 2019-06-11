using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	private GameController gc;
	private CameraShake cs;
	public GameObject explosion, explosion2, explosion3;
	public Transform trans;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			if (!gc.shield) {
				Vector3 offset = new Vector3 (-5.0f, 6.7f, 4.0f);
				Destroy (other.gameObject);
				Destroy (gameObject);
				Quaternion rot = trans.rotation;
				rot.y = -90;
				Instantiate (explosion, trans.position + offset , rot);
				cs.Shake ();
				gc.GameOver();
				// Debug.Log (gameObject.tag + " destroyed by contact with " + other.tag);
			}	
		}

		else if (other.tag == "Invert") {
			gc.invert = true;
			Destroy (other.gameObject);
			Vector3 offset = new Vector3 (-5.0f, 6.7f, 0.0f);
			Quaternion rot = trans.rotation;
			rot.y = -90;
			Instantiate (explosion2, trans.position + offset , rot);
		}

		else if (other.tag == "Shield") {
			gc.shield = true;
			Destroy (other.gameObject);
			Vector3 offset = new Vector3 (-5.0f, 6.7f, 0.0f);
			Quaternion rot = trans.rotation;
			rot.y = -90;
			Instantiate (explosion3, trans.position + offset , rot);
		}
	}

	void Start () {
		GameObject go = GameObject.FindWithTag("GameController");
		if (go) 
			gc = go.GetComponent<GameController>();
		// else
		// 	Debug.Log ("cannot find 'GameController' script");
		go = GameObject.FindWithTag("MainCamera");
		if (go)
			cs = go.GetComponent<CameraShake>();
		// else
		// 	Debug.Log ("cannot find 'CameraShake' script");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public GameObject [] cars;
	public Transform transform;
	public float spawn_wait;
	public float z_pos;
	public float x_min, x_max;
	public float speed;

	private IEnumerator spawn () {

		while (true) {

			GameObject car;
			car = cars [Random.Range (0, cars.Length)];
			Vector3 position = new Vector3 (Random.Range (x_min, x_max), 0.0f, z_pos);
			Instantiate (car, position, transform.rotation);
			yield return new WaitForSeconds (spawn_wait);
		}	
	}

	void Start () {
		Debug.Log (gameObject.tag + " started");
		StartCoroutine( spawn ());
	}

}

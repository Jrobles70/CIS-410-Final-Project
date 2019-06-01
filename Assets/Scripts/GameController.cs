using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

	public GameObject [] cars;
	public GameObject gameover_text;
	public Text score_text;
	public float [] spawn_pos;
	public float spawn_wait;
	public float z_pos;
	public float y_pos = 0.0f;
	public float x_min, x_max;
	private bool gameover = false;
	private float score = 0.0f;

	private IEnumerator spawn () {

		while (!gameover) {

			GameObject car;
			int index = (int) Random.Range (0.0f, 4.0f);
			car = cars [Random.Range (0, cars.Length)];
			Vector3 position = new Vector3 (spawn_pos[index], y_pos, z_pos);
			Instantiate (car, position, transform.rotation);
			yield return new WaitForSeconds (spawn_wait);
			spawn_wait = spawn_wait - 0.001f;
		}

	}

	void Update() {
		if (gameover){
			if (Input.GetKey("space"))
        	{
        	    Application.LoadLevel(0);
        	}
		} else {
			score += Time.deltaTime;
			score_text.text = System.Math.Round(score, 2).ToString("0.00");;
		}
	}

	void Start () {
		// Debug.Log (gameObject.tag + " started");
		StartCoroutine( spawn ());
	}

	public void GameOver () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");
		if (objs.Length > 0) {
			foreach(GameObject go in objs)  {
				MoveRoad level = go.GetComponent<MoveRoad>();
				level.SetVelocity(new Vector3(0.0f, 0.0f, 0.0f));
			}
		}
		else
			Debug.Log ("cannot find 'MoveRoad' script");
		
		gameover = true;
		Debug.Log ("Game Over!");
		gameover_text.SetActive(true);
	}

}



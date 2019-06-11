using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

	public GameObject [] cars;
	public GameObject gameover_panel;
	public GameObject PauseMenu;
	public Text score_text;
    public Text high_score_text;
    public Text powerUpText;
	public Text powerUpType;
    public float high_score = 0.0f;
    public float score_multiplier = 10.0f;
    public float [] spawn_pos;
	public float spawn_wait;
	public float z_pos;
	public float y_pos = 0.0f;
	public float x_min, x_max;
	public float powerUpTime = 5.0f;
	public float speedFactor = 1.0f;
	public bool isPaused = false;
	public bool shield = false;
	public bool invert = false;
    public bool tutorial = false;

    private float score = 0.0f;
	private float t;
	private bool gameover = false;

	public bool isGameOver () {
		return gameover;
	}

	private IEnumerator spawn () {

		while (!gameover) {
			GameObject car;
			int index = (int) Random.Range (0.0f, 4.0f);
			car = cars [Random.Range (0, cars.Length)];
			Vector3 position = new Vector3 (spawn_pos[index], y_pos, z_pos);
			Instantiate (car, position, transform.rotation);
			yield return new WaitForSeconds (spawn_wait);
			if (spawn_wait > 0.5) {
				spawn_wait -= 0.005f;
				speedFactor += 0.005f;
			}
		}
	}

	public void Pause () {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			isPaused = true;
			PauseMenu.SetActive (true);
		}
		else {
			Time.timeScale = 1;
			isPaused = false;
			PauseMenu.SetActive (false);
		}
	}

	public void Timer (string powerUp) {
		powerUpType.text = powerUp;
		powerUpText.text = System.Math.Round(t, 0).ToString("0");
		if (t > 0.0f) {
			t -= Time.deltaTime;
			powerUpText.text = System.Math.Round(t, 0).ToString("0");
		}else {
			t = powerUpTime;
			if (powerUp == "Invert"){
				invert = false;
			}
			else if (powerUp == "Shield") {
				shield = false;
			}
		}
	}

	void Update() {
		if (!gameover) {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")))
                score += Time.deltaTime * score_multiplier;
            else
                score += Time.deltaTime;
            score_text.text = System.Math.Round(score, 2).ToString("0.00");
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Pause ();
			}
			if (invert) {
				Timer ("Invert");
			}
			else if (shield) {
				Timer ("Shield");
			}
			else {
				powerUpText.text = "";
				powerUpType.text = "";
			}
		} else if (!tutorial){
            if (score > high_score)
            {
                high_score = score;
                PlayerPrefs.SetFloat("HighScore", high_score);
            }

            high_score_text.text = high_score.ToString("0.00");
        }
    }

	void Start () {
        high_score = PlayerPrefs.GetFloat("HighScore");
        // Debug.Log (gameObject.tag + " started");
        t = powerUpTime;
		if (cars.Length != 0) {
			StartCoroutine( spawn ());
		}
	}

	public void GameOver () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Level");
		if (objs.Length > 0) {
			foreach(GameObject go in objs)  {
				MoveRoad level = go.GetComponent<MoveRoad>();
				level.SetVelocity(new Vector3(0.0f, 0.0f, 0.0f));
			}
		}
		// else
		// 	Debug.Log ("cannot find 'MoveRoad' script");
		
		gameover = true;
		// Debug.Log ("Game Over!");
		gameover_panel.SetActive(true);
	}

}

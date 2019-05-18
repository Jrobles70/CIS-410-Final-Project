using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject [] cars;
	public float spawn_wait;
	public float z_pos;
	public float x_min, x_max;
    public Text gameOverText;
    public Text scoreText;
    public float [] spawn_pos;

    private float timer;
    private bool gameover = false;


	private IEnumerator spawn () {

		while (!gameover) {
            GameObject car;
			car = cars [Random.Range (0, cars.Length)];
            int x_index = (int) Random.Range(0.0f, 4.0f);
			Vector3 position = new Vector3 (spawn_pos[x_index], 0.0f, z_pos);
			Instantiate (car, position, transform.rotation);
			yield return new WaitForSeconds (spawn_wait);
		}

	}

    void Update()
    {
        if (gameover)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gameover = false;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
            {
                timer += .3f;
            }
            scoreText.text = System.String.Format(((System.Math.Round(timer) == timer) ? "{0:0}" : "{0:0.00}"), timer);

        }
    }


    void Start () {
        // Debug.Log (gameObject.tag + " started");
        timer = 0.0f;
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
        gameOverText.text = "GAME OVER! \n Press R to restart.";
        Debug.Log ("Game Over!");
	}

}

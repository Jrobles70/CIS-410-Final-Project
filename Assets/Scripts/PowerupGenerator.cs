using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
	public float xMin = -23.0f;
	public float xMax = 23.0f;
	public GameObject [] powerUps;
	public float waitTime = 10.0f;
	private GameController gc;

	IEnumerator RandomlyGenerate () {
		yield return new WaitForSeconds (waitTime);
		while (!gc.isGameOver ()) {
			GameObject powerUp = powerUps [ (int) Random.Range (0, powerUps.Length)];
			float x = Random.Range (xMin, xMax);
			Vector3 position = new Vector3 (x, 0.0f, 1000.0f);
			Instantiate (powerUp, position, transform.rotation);
			yield return new WaitForSeconds (waitTime);
		}
	}

    void Start()
    {
    	GameObject go = GameObject.FindWithTag("GameController");
        if (go) 
            gc = go.GetComponent<GameController>();
        StartCoroutine (RandomlyGenerate ());
    }
}

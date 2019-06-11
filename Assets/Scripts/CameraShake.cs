using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float duration = 0.25f;
	public float magnitude = 3.0f;
	private Vector3 originalPos;

	void StartShake () {
		float x = Random.Range (-1.0f, 1.0f) * magnitude;
		float y = Random.Range (-1.0f, 1.0f) * magnitude;
		Vector3 newPos = new Vector3 
		(
			x + originalPos.x, 
			y + originalPos.y,
			originalPos.z
		);
		transform.localPosition = newPos;
	}

	void StopShake () {
		CancelInvoke ("StartShake");
		transform.localPosition = originalPos;
	}

	public void Shake () {
		originalPos = transform.position;
		InvokeRepeating ("StartShake", 0f, 0.005f);
		Invoke ("StopShake", duration);
		transform.position = Vector3.Lerp (transform.position, originalPos, 0.2f);
	}
}

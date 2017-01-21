using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float fadeInTimeout;
	public float speed;
	public float fadeOutSpeed = 0.01f;
	public float fadeInSpeed = 0.1f;

	float disappearChance;
	float lastDissapearRoll=0;

	public bool fadeIn = false;
	public bool started = false;
	public float fadeInTimer;

	Vector3 rotationDirection;
	// Use this for initialization
	void Start () {
		disappearChance = Random.Range (0.02f, 0.5f);
		lastDissapearRoll = Time.time + Random.Range(0f, 1f);
		rotationDirection = Random.insideUnitSphere * 100f;
		fadeOutSpeed = fadeOutSpeed * (Random.value + 0.001f) * 3;
		transform.localScale = new Vector3 (0, 0, 0);
		fadeInTimer = fadeInTimeout;
	}
	
	// Update is called once per frame
	void Update () 
	{			
		float scale = transform.localScale.z;

		if (!fadeIn && started)
		{
			if (scale > 0) 
			{
				scale = Mathf.Lerp (scale, 0, fadeOutSpeed);
				if (scale < 0.01f)
				{
					scale = 0;	
				}
			} else
			{
				Sonar.RemovePoint (this.transform);
				//Destroy (this.gameObject);
			}
		} else if (started)
		{
			if (scale < 1)
			{
				scale = Mathf.Lerp (scale, 1, fadeInSpeed);
				if (1 - scale < 0.01f)
				{
					scale = 1;
				}
			} else
			{
				fadeIn = false;
			}
		}
		transform.localScale = new Vector3 (scale, scale, scale);

		transform.Rotate (rotationDirection * Time.deltaTime);


		fadeInTimer -= speed*Time.deltaTime;
		if (fadeInTimer <= 0f && !started) {
			this.GetComponent<MeshRenderer> ().enabled = true;
			fadeIn = true;
			started = true;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			fadeOutSpeed = 0.4f;

		}

		lastDissapearRoll = Time.time;

	}
}

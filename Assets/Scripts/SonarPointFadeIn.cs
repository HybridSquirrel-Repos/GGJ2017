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

	bool fadeIn = false;
	bool started = false;

	Vector3 rotationDirection;
	// Use this for initialization
	void Start () {
		disappearChance = Random.Range (0.02f, 0.5f);
		lastDissapearRoll = Time.time + Random.Range(0f, 1f);
		rotationDirection = Random.insideUnitSphere * 100f;
		fadeOutSpeed = fadeOutSpeed * (Random.value + 0.001f) * 3;
		transform.localScale = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{			
		float scale = transform.localScale.z;

		if (!fadeIn && started)
		{
			if (scale > 0) 
			{
				scale -= fadeOutSpeed * Time.deltaTime;
			} else
			{
				Destroy (this.gameObject);
			}
		} else if (started)
		{
			if (scale < 0.1)
			{
				scale += fadeInSpeed * Time.deltaTime;
			} else
			{
				fadeIn = false;
			}
		}
		transform.localScale = new Vector3 (scale, scale, scale);

		transform.Rotate (rotationDirection * Time.deltaTime);


		fadeInTimeout -= speed*Time.deltaTime;
		if (fadeInTimeout < 0f && !started) {
			this.GetComponent<MeshRenderer> ().enabled = true;
			fadeIn = true;
			started = true;
		}



		lastDissapearRoll = Time.time;
			
		if (Time.time - lastDissapearRoll > 1f) {
			lastDissapearRoll = Time.time;


			if (Random.value < disappearChance) {
			}
		}

	}
}

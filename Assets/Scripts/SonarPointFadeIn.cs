using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float fadeInTimeout;
	public float speed;

	float disappearChance;
	float lastDissapearRoll=0;


	Vector3 rotationDirection;

	// Use this for initialization
	void Start () {
		disappearChance = Random.Range (0.02f, 0.5f);
		lastDissapearRoll = Time.time + Random.Range(0f, 1f);
		rotationDirection = Random.insideUnitSphere * 100f;
	}
	
	// Update is called once per frame
	void Update () {


		transform.Rotate (rotationDirection * Time.deltaTime);

		fadeInTimeout -= speed*Time.deltaTime;
		if (fadeInTimeout < 0f) {
			this.GetComponent<MeshRenderer> ().enabled = true;
		}
			
		if (Time.time - lastDissapearRoll > 1f) {
			lastDissapearRoll = Time.time;
			if (Random.value < disappearChance) {
				Destroy (this.gameObject);
			}
		}

	}
}

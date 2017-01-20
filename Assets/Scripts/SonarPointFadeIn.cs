using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float fadeInTimeout;
	public float speed;

	float disappearChance;
	float lastDissapearRoll=0;

	// Use this for initialization
	void Start () {
		disappearChance = Random.Range (0.03f, 0.6f);
		lastDissapearRoll = Time.time + Random.Range(0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
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

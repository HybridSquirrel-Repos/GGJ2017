using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float fadeInTimeout;
	public float fadeInTimerSpeed;
	public float fadeOutSpeed;
	public float fadeInSpeed;

	public float maxScale;


	public AnimationCurve scaleAnimation;

	public bool fadingIn = false;
	public bool fadingOut = false;
	public float fadeInTimer;

	float fadeInAnimationTime=0f;
	float currentScale;

	Vector3 rotationDirection;
	// Use this for initialization
	void Start () {
		rotationDirection = Random.insideUnitSphere * 100f;
		fadeOutSpeed = fadeOutSpeed * (Random.value + 0.001f) + 0.1f;
		fadeInTimer = fadeInTimeout;
		currentScale = 0f;
		fadeInAnimationTime = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{		

		//delay in appearing, to simulate speed of sound
		if (!fadingIn && !fadingOut) {
			fadeInTimer -= fadeInTimerSpeed*Time.deltaTime;
			if (fadeInTimer <= 0f) {
				this.GetComponent<MeshRenderer> ().enabled = true;
				fadingIn = true;
			}
		}

		//fade in animation
		if (fadingIn) {
			fadeInAnimationTime += Time.deltaTime;
			currentScale = scaleAnimation.Evaluate (fadeInAnimationTime);
			if (fadeInAnimationTime > 1f) {
				fadingIn = false;
				fadingOut = true;
			}
		}

		//fade out animation, also serves to DESTROY the gameobject
		if (fadingOut) {
			currentScale = Mathf.Lerp (currentScale, 0, fadeOutSpeed * Time.deltaTime);
			if (currentScale < 0.01f){
				Destroy (this.gameObject);
				Sonar.pointCount--;	
			}
		}
			

		var distance = Vector3.Distance (this.transform.position, Camera.main.transform.position);
		Mathf.Clamp (distance, 0f, 19f);
		if (fadingIn || distance < 2f) {
			transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
			transform.Rotate (rotationDirection * Time.deltaTime);
		} else {
			if (Random.value < distance / 20f) {
				transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
				transform.Rotate (rotationDirection * Time.deltaTime);
			}
		}



		if (Input.GetKeyDown (KeyCode.R)) {
			fadingIn = false;
			fadingOut = true;
			fadeOutSpeed = 0.4f;
		}
	}
}

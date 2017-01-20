using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float timeout;

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeout -= speed;
		if (timeout < 0f) {
			this.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}
}

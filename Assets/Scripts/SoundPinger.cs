using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPinger : MonoBehaviour {

	float lastPing;
	public GameObject sonarPointPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V) && Time.time - lastPing > 1f) {
			lastPing = Time.time;

			for (var i = 0; i < 3600; i++) {
				var ray = new Ray (this.transform.position, Random.insideUnitSphere);
				Sonar.ShootRay (ray, sonarPointPrefab);
			}

		}
	}
}

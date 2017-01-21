using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundCaster : MonoBehaviour {

	bool armed;
	public int raycount = 30;
	AudioSource footstepSound;
	public GameObject sonarPointPrefab;

	// Use this for initialization
	void Start () {
		armed = true;
		footstepSound = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (footstepSound.isPlaying) {
			if (armed == true) {
				armed = false;
				//A STEP HAS BEEN MADE
				for (var i = 0; i < raycount; i++){
					var ray = new Ray(this.transform.position, Random.insideUnitSphere);
					Sonar.ShootRay(ray, sonarPointPrefab, 1f);
				}
				for (var i = 0; i < raycount; i++){
					var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward*0.1f + (Random.insideUnitSphere*0.13f));
					Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward, Color.green, 5f);
					Sonar.ShootRay (ray, sonarPointPrefab, 4f);
				}

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    new Noise(transform.position, 10);
                else
                    new Noise(transform.position, 5);

			}
		}else{
			armed = true;
		}
	}
}

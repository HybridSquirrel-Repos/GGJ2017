using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour {

	public GameObject sonarPointPrefab;

	public int pointCount;

	void sonar(){
		var me = Camera.main.transform;
		RaycastHit hit;

		for (var x = 0; x < Camera.main.pixelWidth; x += 20) {
			for (var y = 0; y < Camera.main.pixelHeight; y += 20) {
				var ray = Camera.main.ScreenPointToRay (new Vector3 (x, y, 0f));
				if (Physics.Raycast (ray, out hit)) {
					var sonarPoint = GameObject.Instantiate (sonarPointPrefab, hit.point, Quaternion.identity);
					sonarPoint.GetComponent<SonarPointFadeIn> ().timeout = hit.distance;
					sonarPoint.transform.rotation = Quaternion.LookRotation (hit.normal);
					Debug.DrawRay (hit.point, hit.normal*0.1f, Color.magenta, 5f);
					pointCount++;
				}
			}
		}

		Debug.Log (pointCount);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			sonar ();
		}
	}
}

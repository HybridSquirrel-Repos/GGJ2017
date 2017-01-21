using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour {

	public static int pointCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public static void ShootRay(Ray ray, GameObject sonarPointPrefab){
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			//HIT OBJECT, SPAWN A SONAR POINT
			var sonarResponder = hit.collider.gameObject.GetComponent<SonarResponder> ();
			if (sonarResponder != null && hit.collider.tag != "Enemy" && false) {
				var sonarPoint = GameObject.Instantiate (sonarPointPrefab, hit.point, Quaternion.identity);
				sonarPoint.GetComponent<SonarPointFadeIn> ().fadeInTimeout = hit.distance;
				sonarPoint.GetComponent<MeshRenderer> ().material = hit.collider.gameObject.GetComponent<SonarResponder> ().mat;

				//this should be in the SONAR POINT script
				sonarPoint.transform.rotation = Random.rotation;
				var scale = Random.Range (0.5f, 1.5f);
				sonarPoint.transform.localScale *= scale;

				//DEBUGGING ONLY
				pointCount++;
			} else if (hit.collider.tag == "Enemy")
			{
				// Get the render object of the enemy
				/*Debug.Log ("Enemy Created");
				GameObject renderEnemy = hit.collider.transform.GetChild (0).gameObject;
				GameObject copy = GameObject.Instantiate (renderEnemy, renderEnemy.transform.position, Quaternion.identity);
				copy.GetComponent <Renderer> ().enabled = true;*/
				hit.collider.GetComponent <Object_Clone> ().Clone ();
			}
		}
		//more debugging
		Debug.DrawRay(ray.origin, ray.direction*5f, Color.magenta, 5f);

	}
}

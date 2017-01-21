using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour {

	public int displayCount;
	public static int pointCount;

	public static int MAX_POINTS = 16000;
	public static int MAP_SIZE_X = 105;
	public static int MAP_SIZE_Y = 105;
	public static int MAP_SIZE_Z = 105;
	public static int MAX_CUBE_POINTS = 100;


	static Text debugPointCountText;

	public static int[] map;
	// Use this for initialization
	void Start () {
		map = new int[MAP_SIZE_X * MAP_SIZE_Y * MAP_SIZE_Z];
		debugPointCountText = GameObject.Find ("pointCountText").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		debugPointCountText.text = pointCount.ToString();
	}


	public static void ShootRay(Ray ray, GameObject sonarPointPrefab, float volume = 4){
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, volume)) {
			//HIT OBJECT, SPAWN A SONAR POINT
			var sonarResponder = hit.collider.gameObject.GetComponent<SonarResponder> ();
			 if (hit.collider.tag == "Enemy")
			{
				// Get the render object of the enemy
				/*Debug.Log ("Enemy Created");
				GameObject renderEnemy = hit.collider.transform.GetChild (0).gameObject;
				GameObject copy = GameObject.Instantiate (renderEnemy, renderEnemy.transform.position, Quaternion.identity);
				copy.GetComponent <Renderer> ().enabled = true;*/
				hit.collider.GetComponent <Object_Clone> ().Clone ();
			} else if (hit.collider.tag == "SoundGenerator")
			{
				print ("SoundGenerator yes sir");
				hit.collider.GetComponent <Renderer> ().enabled = true;
			} else if (sonarResponder != null) {

				Vector3 roundedPoint = RoundVector (hit.point);
				if (map[ListPos (roundedPoint)] >= MAX_CUBE_POINTS || pointCount > MAX_POINTS)
				{
					return;
				}

				GameObject sonarPoint = GameObject.Instantiate (sonarPointPrefab, hit.point, Quaternion.identity);
				sonarPoint.GetComponent<SonarPointFadeIn> ().fadeInTimeout = hit.distance;
				sonarPoint.GetComponent<MeshRenderer> ().material = hit.collider.gameObject.GetComponent<SonarResponder> ().mat;

				//DEBUGGING ONLY
				pointCount++;
			}
		}
		//more debugging
		Debug.DrawRay(ray.origin, ray.direction*5f, Color.magenta, 5f);

	}


	public static Vector3 RoundVector (Vector3 pos)
	{
		return new Vector3 (Mathf.RoundToInt (pos.x), Mathf.RoundToInt (pos.y), Mathf.RoundToInt (pos.z));
	}

	public static int ListPos(Vector3 vec)
	{
		return (int)vec.x + (int)vec.y * (int)MAP_SIZE_X + (int)vec.z * (int)MAP_SIZE_X * MAP_SIZE_Y; 
	}
}

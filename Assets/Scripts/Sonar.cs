using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour {

    public int displayCount;
    public static int pointCount;

    public static int MAX_POINTS = 1000;
    public static int MAP_SIZE_X = 105;
    public static int MAP_SIZE_Y = 105;
    public static int MAP_SIZE_Z = 105;
    public static int MAX_CUBE_POINTS = 25;
    public static List<Transform> points = new List<Transform>();
    public static List<Transform> pool = new List<Transform>();

    static Text debugPointCountText;

    public static int[] map;

    static Sonar()
    {
        map = new int[MAP_SIZE_X * MAP_SIZE_Y * MAP_SIZE_Z];
    }

    void Start()
    {
        debugPointCountText = GameObject.Find("pointCountText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
		if (pointCount > MAX_POINTS) {
			int pointsToRemove = pointCount - MAX_POINTS;
			for (int i = 0; i < pointsToRemove; i++) {
				RemovePoint (points[i]);
			}
		}


		debugPointCountText.text = pointCount.ToString();
	}


	public static void ShootRay(Ray ray, GameObject sonarPointPrefab)
    {
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			//HIT OBJECT, SPAWN A SONAR POINT
			var sonarResponder = hit.collider.gameObject.GetComponent<SonarResponder> ();
			if (sonarResponder != null && hit.collider.tag != "Enemy") {


				GameObject sonarPoint = MakePoint (sonarPointPrefab, hit.point, Random.rotation);
				if (sonarPoint == null) {
					return;
				}

				//sonarPoint = GameObject.Instantiate (sonarPointPrefab, hit.point, Quaternion.identity);

				sonarPoint.GetComponent<SonarPointFadeIn> ().fadeInTimeout = hit.distance;
				sonarPoint.GetComponent<MeshRenderer> ().material = hit.collider.gameObject.GetComponent<SonarResponder> ().mat;
				points.Add (sonarPoint.transform);
				//this should be in the SONAR POINT script
				//sonarPoint.transform.rotation = Random.rotation;
				var scale = Random.Range (0.5f, 1.5f);
				sonarPoint.transform.localScale *= scale;

	

				//DEBUGGING ONLY
				pointCount++;
			}
            else if (hit.collider.tag == "Enemy") {
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

	public static void RemovePoint(Transform point)
	{
		//GameObject.Destroy (point.gameObject);
		if (pool.Count <= MAX_POINTS) {
			point.gameObject.SetActive (false);
			pool.Add (point);

		}
        else {
			Destroy (point.gameObject);
		}
		points.Remove (point);
		pointCount--;
		map [ListPos (RoundVector (point.position))]--;
	}

	public static GameObject MakePoint(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Vector3 roundedPoint = RoundVector (position);
        int index = ListPos(roundedPoint);
        if (map == null)
            Debug.LogError("REEEE The map array is null!");
        if (map[index] >= MAX_CUBE_POINTS) {
			print("Oh no you didn't");	
			return null;
		}
		map [index]++;
        pointCount++;

		if (pool.Count > 0) {
			GameObject point = pool [0].gameObject;
			pool.Remove(pool[0]);
			point.transform.position = position;
			point.transform.rotation = rotation;
			var point_fade_in = point.GetComponent<SonarPointFadeIn>();
			point_fade_in.fadeIn = point.GetComponent <SonarPointFadeIn> ().started = false;
			point_fade_in.fadeInTimer = point.GetComponent <SonarPointFadeIn> ().fadeInTimeout;
			point.gameObject.SetActive (true);
			return point;

		} else
		{
			return GameObject.Instantiate (prefab, position, rotation);
		}

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

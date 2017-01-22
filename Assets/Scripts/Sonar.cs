using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour {

	public int displayCount;
	public Material floorMaterial;
	public static int pointCount;

	public static int MAX_POINTS = 25000;
	public static int MAP_SIZE_X = 105; 
	public static int MAP_SIZE_Y = 105;
	public static int MAP_SIZE_Z = 105;
	public static int MAX_CUBE_POINTS = 100;


	static Text debugPointCountText;

	static LayerMask ignorePlayer = new LayerMask();
	static LayerMask alsoHitPlayer = new LayerMask();

	public static int[] map;
	// Use this for initialization
	void Start () {
		map = new int[MAP_SIZE_X * MAP_SIZE_Y * MAP_SIZE_Z];
		debugPointCountText = GameObject.Find ("pointCountText").GetComponent<Text>();
		ignorePlayer = 1;
		alsoHitPlayer = 1 | (1 << 8);
	}

	// Update is called once per frame
	void Update () {
		debugPointCountText.text = pointCount.ToString();

	}

	public static void ShootRay(Ray ray, GameObject sonarPointPrefab, float volume = 4){
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, volume, ignorePlayer)) {
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
				hit.collider.GetComponent <Sound_Generator> ().Show ();
			} else if (sonarResponder != null) {
				Vector3 roundedPoint = RoundVector (hit.point);
				if (map[ListPos (roundedPoint)] >= MAX_CUBE_POINTS || pointCount > MAX_POINTS)
				{
					return;
				} 

				//checkVisibility
				if (Vector3.Distance (ray.origin, Camera.main.transform.position) > 3f) {
					//ray assumed not to originate from player or nearest vecinity, so only will be rendered if there is a straight line to the player
					RaycastHit reverseHit;
					Ray reverseRay = new Ray (hit.point + Vector3.up * 0.1f, (Camera.main.transform.position - hit.point).normalized);
					//reverseRay.direction = (Camera.main.posi)
					Physics.Raycast(reverseRay,out reverseHit, 100f, alsoHitPlayer);
                    //Debug.DrawLine (reverseRay.origin, reverseHit.point, Color.cyan, 1f);

                    if (reverseHit.transform != null)
                    {
                        if (reverseHit.transform.CompareTag("Player") == false)
                        {
                            //reverse ray DID NOT hit player, so he can't hear this echo
                            Debug.DrawLine(reverseRay.origin, reverseHit.point, Color.red, 10f);
                            return;
                        }
                        else
                        {
                            Debug.DrawLine(reverseRay.origin, reverseHit.point, Color.green, 10f);
                        }
                    }
				}

				GameObject sonarPoint = GameObject.Instantiate (sonarPointPrefab, hit.point, Quaternion.identity);
				sonarPoint.GetComponent<SonarPointFadeIn> ().fadeInTimeout = hit.distance;
				float c = SignedAngleBetween (hit.normal, Vector3.up, Vector3.up) / 90;

				Color color = new Color (1, 1-c, 1-c);
				sonarPoint.GetComponent <MeshRenderer> ().material.color = color;
			
				//DEBUGGING ONLY
				pointCount++;
			}
		}
		//more debugging
		//Debug.DrawRay(ray.origin, ray.direction*5f, Color.magenta, 5f);

	}


	public static Vector3 RoundVector (Vector3 pos)
	{
		return new Vector3 (Mathf.RoundToInt (pos.x), Mathf.RoundToInt (pos.y), Mathf.RoundToInt (pos.z));
	}

	public static int ListPos(Vector3 vec)
	{
		return (int)vec.x + (int)vec.y * (int)MAP_SIZE_X + (int)vec.z * (int)MAP_SIZE_X * MAP_SIZE_Y; 
	}

	public static float AngleToUp(Vector3 vec)
	{
		return Mathf.Acos (Vector3.Dot (vec, Vector3.up));

	}

	public static float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
	{
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));

		// angle in [-179,180]
		float signed_angle = angle * sign;

		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;

		return signed_angle;
	}
}

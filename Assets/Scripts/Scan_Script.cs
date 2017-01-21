using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan_Script : MonoBehaviour
{
	float range = 10;
	float scanSpeed = 10;
	float scaleSpeed = 10;
	bool scanning = false;
	Vector3 start;


	List<GameObject> actives = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			scanning = true;
			start = transform.position;
		}


		if (Input.GetKeyDown (KeyCode.R))
		{
			foreach (GameObject active in actives)
			{
				active.GetComponent <Renderer> ().enabled = false;
			}
		}

		if (scanning)
		{
			transform.Translate (0, scanSpeed * Time.deltaTime, 0);
			float scale = transform.localScale.z;
			scale += scanSpeed * Time.deltaTime;
			transform.localScale = new Vector3 (scale, scale, scale);
			if (Vector3.Distance (start, transform.position) >= range)
			{
				scanning = false;
				transform.localScale = Vector3.zero;
				transform.localPosition = Vector3.zero;
			}
		}
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.GetComponent <Renderer> () != null)
		{
			coll.gameObject.GetComponent <Renderer> ().enabled = true;
			actives.Add (coll.gameObject);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_MaterialDetectionScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
	
		/* Raycast down to see if we can find any floor */	
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 5))
		{
			if (hit.collider.tag == "Floor")
			{
				string mat = hit.collider.GetComponent <Floor_Material> ().material;
				print ("Walking on " + mat);
			}
		}
	}
}

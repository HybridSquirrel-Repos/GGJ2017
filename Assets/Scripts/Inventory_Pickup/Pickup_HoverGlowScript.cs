using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_HoverGlowScript : MonoBehaviour
{
	public Camera camera;
	public Transform armPickupPos;

	public float pickupDistance = 10f;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Debug.DrawRay (ray.origin, ray.direction, Color.magenta, 5f);

			if (Physics.Raycast (ray, out hit, pickupDistance))
			{			print ("Mb0");
				
				if (hit.collider.GetComponent <Pickup>() != null && GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Inventory_System>().currentPickup == null)
				{
					hit.collider.GetComponent <Pickup> ().PickUp (armPickupPos);
				}
			}

		}
	}
}

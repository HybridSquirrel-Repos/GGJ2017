using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_System : MonoBehaviour 
{

	public GameObject currentPickup = null;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Q))
		{
			if (currentPickup != null)
			{
				currentPickup.GetComponent <Pickup> ().DropPickup ();
				currentPickup = null;
			}
		}
	}
}

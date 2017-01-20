using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
	public float pickupSpeed = 0.5f;
	public string pickupName = "Pickup";
	public Text pickupText;

	Rigidbody rb;
	bool pickup = false;
	bool fadeIn = true;
	Transform parent;

	void Start()
	{
		rb = GetComponent <Rigidbody> ();
	}


	void Update()
	{
		if (pickup)
		{
			transform.position = Vector3.Lerp (transform.position, parent.position, pickupSpeed);
		
			Color c = pickupText.color;
			if (fadeIn)
			{
				c.a += 0.01f;
				if (c.a >= 1)
					fadeIn = false;
			} else
			{
				c.a -= 0.01f;
			}
			pickupText.color = new Color (c.r, c.g, c.b, c.a);
		}
	}


	public void DropPickup()
	{
		pickup = false;
		pickupText.color = new Color (pickupText.color.r, pickupText.color.g, pickupText.color.b, 0);
		transform.SetParent (null);
		rb.constraints = RigidbodyConstraints.None;
	}

	public void PickUp(Transform parent)
	{
		this.parent = parent;
		pickup = true;
		fadeIn = true;
		transform.SetParent (parent);
		rb.constraints = RigidbodyConstraints.FreezeAll;
		pickupText.text = pickupName;

		GameObject.FindGameObjectWithTag ("Inventory").GetComponent <Inventory_System> ().currentPickup = this.gameObject;
	}
}

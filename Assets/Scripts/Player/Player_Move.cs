using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour 
{
	public float moveSpeed = 5;
	public float maxSpeed = 10;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float hor = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		float ver = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;


	}

	void OnCollisionEnter(Collision coll)
	{
		
	}
}

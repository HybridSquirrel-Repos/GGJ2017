using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenAnimation : MonoBehaviour 
{
	bool playing = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			Play ();
		}
	}

	public void Play()
	{
//		playing = true;
//		foreach (GameObject point in GameObject.FindGameObjectsWithTag ("SonarPoint"))
//		{
//			point.GetComponent <SonarPointFadeIn> ().goal = transform.position;
//		}
	}
}

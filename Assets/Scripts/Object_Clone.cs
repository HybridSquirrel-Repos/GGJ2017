using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Clone : MonoBehaviour 
{
	public float cloneTimer = 3f;
	public Vector3 cloneOffset = Vector3.zero;
	public GameObject cloneObject;

	float cloneTime = 0f;

	// Use this for initialization
	void Start () 
	{
		if (cloneObject == null)
		{
			cloneObject = this.gameObject;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		if (cloneTime > 0f)
		{
			cloneTime -= Time.deltaTime;
		}
	}

	public void Clone()
	{
		if (cloneTime <= 0f) 
		{
			print ("Object_Clone.cs: Clone Timer is " + cloneTime);
			GameObject clone = GameObject.Instantiate (cloneObject, transform.position, transform.rotation);
			clone.SetActive (true);
			Renderer renderer = clone.GetComponent <Renderer> ();
			renderer.enabled = true;
			Color rendererColor = renderer.material.color;
			rendererColor.a = 255;
			cloneTime = cloneTimer;


		}
	}
}

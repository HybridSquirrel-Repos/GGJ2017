using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Clone : MonoBehaviour 
{
	public float cloneTimer = 1.5f;
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawCube (transform.position, new Vector3 (1, 2, 1));

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
			rendererColor.a = 1;
			cloneTime = cloneTimer;


		}
	}
}

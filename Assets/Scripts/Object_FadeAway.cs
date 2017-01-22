using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_FadeAway : MonoBehaviour
{
	public float fadeSpeed = 0.001f;

	public float alpha = 1;


	Material mat;


	// Use this for initialization
	void Start () 
	{
		mat = GetComponent <Renderer> ().material;
		Color c = mat.color;
		c.a = 1;
		mat.color = c;
//		print ("Hello there!");
	}
	
	// Update is called once per frame
	void Update () 
	{

		/* Start fading */
		Color color = mat.color;
		color.a = Mathf.Lerp (color.a, 0, fadeSpeed);
		mat.color = color;
		alpha = color.a;
	
	}
}

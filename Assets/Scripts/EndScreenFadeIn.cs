using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreenFadeIn : MonoBehaviour
{

	Image logo;
	// Use this for initialization
	void Start () {
		logo = GetComponent <Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Color c = logo.color;
		c.a = Mathf.Lerp (c.a, 1, 0.01f);
		logo.color = c;
	}
}

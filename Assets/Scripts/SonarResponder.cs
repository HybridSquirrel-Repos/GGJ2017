using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarResponder : MonoBehaviour {

	public Material mat;
	public bool specialColor;
	public Color color;

	void Start()
	{
		if (GetComponent <Renderer> () != null)
		{
			GetComponent <Renderer> ().enabled = false;
		}

		if (GetComponent <Terrain> () != null)
		{
			GetComponent <Terrain> ().enabled = false;
		}
	}
}

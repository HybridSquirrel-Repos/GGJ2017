using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarResponder : MonoBehaviour {

	public Material mat;

	void Start()
	{
		if (GetComponent <Renderer> () != null)
		{
			GetComponent <Renderer> ().enabled = false;
		}
	}
}

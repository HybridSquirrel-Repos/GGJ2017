using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarResponder : MonoBehaviour {

	public Material mat;

	void Start ()
    {
		this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
}

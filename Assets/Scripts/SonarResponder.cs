using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarResponder : MonoBehaviour {

	public Material mat;

	// Use this for initialization
	void Start () {
             this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

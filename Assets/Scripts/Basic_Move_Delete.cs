using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Move_Delete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, 1 * Time.deltaTime);
	}
}

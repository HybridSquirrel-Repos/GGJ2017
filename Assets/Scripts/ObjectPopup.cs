using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPopup : MonoBehaviour {

    public GameObject objToHide;
    public float decay = 3f;

    void Hide() {
        objToHide.SetActive(false);
    }

    public void Popup() {
        CancelInvoke();
        objToHide.SetActive(true);
        Invoke("Hide", decay);
    }


	// Use this for initialization
	void Start () {
        objToHide = transform.Find("radio").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

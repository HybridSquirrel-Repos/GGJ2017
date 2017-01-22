using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterScene : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<Button>().onClick = NextLevel;
    }

    Button.ButtonClickedEvent NextLevel()
    {
        return null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
	public static List<GameObject> resets = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public static void SubscribeToReset(GameObject go)
	{
		resets.Add (go);
	}

	public static void ResetGame()
	{
		foreach (GameObject go in resets)
		{
			go.SendMessage ("ResetGame");
		}

	}

}

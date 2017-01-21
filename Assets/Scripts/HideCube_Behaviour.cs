using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCube_Behaviour : MonoBehaviour
{

	public float revealSpeed = 5f;

	Transform player;

	bool revealing = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (player);	
		if (revealing)
		{
			float scale = transform.localScale.z;
			scale -= revealSpeed * Time.deltaTime;
			if (scale <= 0)
			{
				scale = 0;
				revealing = false;
			}
			
			transform.localScale = new Vector3 (scale, scale, scale);
			transform.Translate (0, 0, (-revealSpeed / 8) * Time.deltaTime);

		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			revealing = true;
		}
	}


	void Reveal()
	{	
		revealing = true;
	}
}

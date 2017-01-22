using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour 
{

	public RawImage redStart;
	RawImage img;
	RawImage start;

	float imgOrig = 0;
	float startOrig = 0;

	Color startGoalColor = new Color(1, 1, 1, 1);

	bool hover = false;

	bool fadeOut = false;

	float startFadeSpeed = 0.03f;

	// Use this for initialization
	void Start () 
	{
		img = transform.GetChild (0).GetComponent <RawImage> ();
		img.rectTransform.Translate (0, 20, 0);
		imgOrig = img.rectTransform.position.y;
		start = transform.GetChild (1).GetComponent <RawImage> ();
		start.rectTransform.Translate (0, 40, 0);
		startOrig = start.rectTransform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!fadeOut)
		{
			if (img.color.a < 1)
			{
				Color c = img.color;
				c.a = Mathf.Lerp (c.a, 1, 0.03f);
				if (c.a >= 0.95f)
				{
					c.a = 1;
				}
				img.color = c;
			} else /*if (start.color.a < 1)*/
			{
				Color c = start.color;
				c = Color.Lerp (c, startGoalColor, startFadeSpeed);

				if (c.a >= 0.95f)
				{
					c.a = 1;
				}
				start.color = c;
			}
		} else
		{
			img.color = Color.Lerp (img.color, Color.clear, 0.1f);
			start.color = Color.Lerp (start.color, Color.clear, 0.1f);
			if (img.color.a <= 0.05f)
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			}
		}
		Vector3 pos = img.GetComponent <RectTransform> ().position;
		img.GetComponent <RectTransform> ().position = Vector3.Lerp (pos, new Vector3(pos.x, imgOrig-20, pos.z), 0.05f);

		Vector3 pos2 = start.rectTransform.position;
		start.rectTransform.position = Vector3.Lerp (pos2, new Vector3 (pos2.x, startOrig - 40, pos.z), 0.01f);
	
		if (Input.GetMouseButtonDown (0) && hover)
		{
			fadeOut = true;

		}
	}

	void ToRed()
	{
		if (start.color.a == 1)
		{
			startFadeSpeed = 0.3f;
			startGoalColor = Color.red;
			hover = true;
		}
		/*Color c = start.color;
		c.r = 255;
		c.g = 0;
		c.b = 0;
		start.color = c;*/
	}

	public void ToWhite()
	{
		if (start.color.a == 1)
		{
			startFadeSpeed = 0.3f;
			startGoalColor = Color.white;
			hover = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour 
{
	/// <summary>
	/// How often (in secs) we should check if we have won
	/// </summary>
	public float winCheckInterval = 5f;

	/// <summary>
	/// The object that we fade in/out
	/// </summary>
	public Image panel;

	/// <summary>
	/// A list of all the sound generators in the scene / world. Found dynamically
	/// </summary>
	private List<Sound_Generator> soundGenerators = new List<Sound_Generator>();


	private float timeUntilNextWinCheck = 0f; 

	private bool fadeInOverlay = false;

	// Use this for initialization
	void Start () 
	{
		/* Find all the objects with the soundgenerator tag */
		GameObject[] generatorObjects = GameObject.FindGameObjectsWithTag ("SoundGenerator");
		foreach (GameObject generator in generatorObjects)
		{
			soundGenerators.Add (generator.GetComponent <Sound_Generator>());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeUntilNextWinCheck > 0)
		{
			timeUntilNextWinCheck -= Time.deltaTime;
		} else
		{
			timeUntilNextWinCheck = winCheckInterval;

			/* Loop through all the generators and check if everyone is activated */
			bool allActivated = true;
			foreach (Sound_Generator generator in soundGenerators)
			{
				if (!generator.isActive)
				{
					allActivated = false;
				}
			}
            if (soundGenerators.Count < 1)
                allActivated = false;

			if (allActivated)
			{
				fadeInOverlay = true;
			}
		}


		if (fadeInOverlay)
		{
			Color c = panel.color;
			c.a = Mathf.Lerp (c.a, 1, 0.05f);
			panel.color = c;

			if (c.a >= 0.95f)
			{
				c.a = 1;
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
                //panel.color = new Color(0, 0, 0, 0);
            }
		}

	}
}

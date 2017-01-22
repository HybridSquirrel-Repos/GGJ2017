using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenAnimation : MonoBehaviour 
{
	/// <summary>
	/// The Object you use to 
	/// </summary>
	public GameObject scareObject;

	/// <summary>
	/// The animator controller
	/// </summary>
	public Animator ac;

	/// <summary>
	/// Time until we should fade in the dark overlay and reset the level
	/// </summary>
	public float timeUntilScreenFadeOut = 2f;

	public Image fadeOutPanel;

	private float fadeOutTimer = 0f;

	private float fadeInTimer = 0f;

	bool playing = false;
	// Use this for initialization
	void Start ()
	{
		fadeInTimer = timeUntilScreenFadeOut;
        fadeOutPanel = GameObject.FindGameObjectWithTag("Mandatory").transform.Find("Canvas/Panel").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.N))
		{
			Play ();
		}
		if (playing)
		{
			if (fadeOutTimer > 0)
			{
				fadeOutTimer -= Time.deltaTime;
			} else
			{
				// Fade in overlay
				float alpha = Mathf.Lerp(fadeOutPanel.color.a, 1, 0.03f);
				Color c = new Color (fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.g, alpha);
				fadeOutPanel.color = c;

				if (alpha >= 0.95)
				{
					// Reset scene
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
				}
			}
		} else
		{
			if (fadeInTimer > 0)
			{
				fadeInTimer -= Time.deltaTime;
			} else
			{
				// Fade out overlay
				float alpha = Mathf.Lerp(fadeOutPanel.color.a, 0, 0.03f);
				Color c = new Color (fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.g, alpha);
				fadeOutPanel.color = c;

				if (alpha <= 0.05)
				{
					// Reset scene
					alpha = 0;
				}
			}
		}
	}

	public void Play()
	{
		scareObject.SetActive (true);
		fadeOutTimer = timeUntilScreenFadeOut;
		playing = true;
		ac.SetTrigger ("attack");

	}
}

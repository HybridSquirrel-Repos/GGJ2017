using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenAnimation : MonoBehaviour 
{
	public static bool dead = false;
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
	public float timeUntilScreenFadeOut = 0.5f;

	/// <summary>
	/// The panel that we fade out
	/// </summary>
	public Image fadeOutPanel;

	private float fadeOutTimer = 0f;

	private float fadeInTimer = 0f;

	private Image fadeObject;

	private int loadSceneIndex = 0;

	bool playing = false;
	// Use this for initialization
	void Start ()
	{
		dead = false;

		fadeInTimer = timeUntilScreenFadeOut;
        fadeOutPanel = GameObject.FindGameObjectWithTag("Mandatory").transform.Find("Canvas/Panel").GetComponent<Image>();
		fadeObject = fadeOutPanel;
        fadeOutPanel.color = Color.white;
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
				float alpha = Mathf.Lerp(fadeOutPanel.color.a, 1, 0.3f);
				Color c = new Color (fadeOutPanel.color.r, fadeOutPanel.color.g, fadeOutPanel.color.g, alpha);
				fadeObject.color = c;

				if (alpha >= 0.95)
				{
					// Reset scene
					if (this.loadSceneIndex == SceneManager.GetActiveScene ().buildIndex)
						Reset.ResetGame ();


					SceneManager.LoadScene (this.loadSceneIndex);
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
				fadeObject.color = c;

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
		if (!playing)
		{
			scareObject.SetActive (true);
			fadeOutTimer = timeUntilScreenFadeOut;
			playing = true;
			ac.SetTrigger ("attack");
			dead = true;
			fadeObject = fadeOutPanel;
			loadSceneIndex = SceneManager.GetActiveScene ().buildIndex;

		}
	}


}

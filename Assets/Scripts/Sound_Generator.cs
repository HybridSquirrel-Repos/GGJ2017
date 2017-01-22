using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Generator : MonoBehaviour 
{
	/// <summary>
	/// The maximum distance between the player and the
	/// sound generator for the player to be able to activate it.
	/// </summary>
	public float activateDistance = 3f;

	/// <summary>
	/// The time between each "pulse" of sound,
	/// if we are active only
	/// </summary>
	public float soundPulseInterval = 2f;

	/// <summary>
	/// How fast we fade in/out (used in lerp as %)
	/// </summary>
	public float fadeSpeed = 0.05f;

	/// <summary>
	/// Volume of the sound
	/// </summary>
	public float volume = 20;

	/// <summary>
	/// If the sound generator is emitting sound
	/// </summary>
	public bool isActive = false;

	/// <summary>
	/// Amount of rays
	/// </summary>
	public int rayCount = 500;

	/// <summary>
	/// What color we should be if we are unactive
	/// </summary>
	public Color unactiveColor = Color.red;

	/// <summary>
	/// What color we should be if we are active
	/// </summary>
	public Color activeColor = Color.green;

	/// <summary>
	/// The sonar ping object.
	/// </summary>
	public GameObject sonarPingObject;

    /// <summary>
    /// Is set to true if the bots don't care about it anymore.
    /// </summary>
    public bool ineffective = false;

    /// <summary>
    /// The time until next pulse.
    /// </summary>
    private float timeUntilNextPulse = 0f;

	/// <summary>
	/// The material of the object
	/// </summary>
	private Material mat;

	private float timeUntilFadeOut = 0;



	/// <summary>
	/// What alpha we are currently trying to reach
	/// </summary>
	private float goalAlpha = 0f;
	
	// Update is called once per frame
	void Update () 
	{

		if (isActive)
		{
			if (timeUntilNextPulse > 0)
			{
				timeUntilNextPulse -= Time.deltaTime;
			} else
			{
				/* Send out a spherical sound */
				PlayerSonarPinger.sonar (sonarPingObject, transform, rayCount, volume, !ineffective);
				timeUntilNextPulse = soundPulseInterval;
			}
		}
		/* Check if player has pressed space */
		if (Input.GetKeyDown (KeyCode.T))	
		{
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
			if (Vector3.Distance (transform.position, player.position) <= activateDistance)
			{
				/* Invert the active state */
				isActive = !isActive;
			}
		}
	}
}

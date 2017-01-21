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
	public int rayCount = 1500;

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
	/// The time until next pulse.
	/// </summary>
	private float timeUntilNextPulse = 0f;

	/// <summary>
	/// The material of the object
	/// </summary>
	private Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent <Renderer> ().material;
		mat.color = (isActive) ? activeColor : unactiveColor;
	}
	
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
				PlayerSonarPinger.sonar (sonarPingObject, transform, rayCount, volume);
				timeUntilNextPulse = soundPulseInterval;
				print ("sound");
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

				/* Update the color of the material according to the state of the sound generator */
				mat.color = (isActive) ? activeColor : unactiveColor;

			}
		}
	}

}

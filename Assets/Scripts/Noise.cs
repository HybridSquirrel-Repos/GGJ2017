using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise 
{
	/* Decay per second */
	const float DECAY_RATE = 10;

	/* Multiplier from the noise to in game units */
	const float NOISE_RADIUS_MULTIPLIER = 10;

	/* Where the noise was made */
	public Vector3 origin;

	/* How loud the noise was */
	public float volume;

	/* When the noise was made (used for calculating decay) */
	public float timeStamp;


	public Noise(Vector3 origin, float volume)
	{
		MakeNoise (origin, volume);
	}

	/* Make a new noise, reseting everything */
	public void MakeNoise (Vector3 origin, float volume)
	{
		this.origin = origin;
		this.volume = volume;
		this.timeStamp = Time.time;
	}


	/* Distance from an object to the edge of the noise radius */
	public void DistanceToNoiseFrom(Vector3 point)
	{
		float distance = Vector3.Distance (this.origin, point);
		float timeDiff = Time.time - this.timeStamp;

		float decayedNoise = this.volume - timeDiff * DECAY_RATE;
		return distance - (decayedNoise * NOISE_RADIUS_MULTIPLIER);
	}



}

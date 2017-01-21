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
        foreach (var ai in PlayerBotDisturber.ai_list) {
            if (Vector3.Distance(origin, ai.transform.position) * 1.25f <= volume)
                ai.heard_noises.Add(this);
        }
    }

    /// <summary>
    /// Get how priortiezed this noise should be for the bot.
    /// </summary>
    /// <param name="from">Where the bot is. </param>
    /// <returns>Priority</returns>
    public float CalcPriority(Vector3 from)
    {
        float distance = Vector3.Distance(this.origin, from);
        float timeDiff = Time.time - this.timeStamp;

        float decayedNoise = this.volume - timeDiff * DECAY_RATE;
        return distance - (decayedNoise * NOISE_RADIUS_MULTIPLIER);
    }

    /*static void DoNoise(Vector3 from, float volume)
    {
        var noise = new Noise(from, volume);

        for (var i = 0; i < 3600; i++)
        {
            var ray = new Ray(this.transform.position, Random.insideUnitSphere);
            Sonar.ShootRay(ray, sonarPointPrefab);
        }

    }*/

}

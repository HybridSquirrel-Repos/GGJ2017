using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Noise 
{
	/* Decay per second */
	public const float DECAY_AFTER = 4.5f;

	/* Multiplier from the noise to in game units */
	const float NOISE_RADIUS_MULTIPLIER = 1;

    const float AI_HEARING = 4f;

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
        this.origin.y = 1.1f;
		this.volume = volume;
		this.timeStamp = Time.time;
        foreach (var ai in PlayerBotDisturber.ai_list) {
            if (Vector3.Distance(origin, ai.transform.position) <= volume * AI_HEARING)
                ai.heard_noises.Add(this);
        }
    }

    /// <summary>
    /// Get how priortiezed this noise should be for the bot.
    /// </summary>
    /// <param name="from">Where the bot is. </param>
    /// <returns>Priority (bigger = better)</returns>
    public float CalcPriority(Vector3 from)
    {
        float distance = Vector3.Distance(this.origin, from);
        float timeDiff = Time.time - this.timeStamp;
        return 100 - timeDiff - distance / 10;
    }
}

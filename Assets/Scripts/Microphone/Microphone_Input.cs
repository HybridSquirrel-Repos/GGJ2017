using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Microphone_Input : MonoBehaviour {
	public float requiredScanVolume = 0.2f;
	public float scanInterval = 5;
	AudioSource source;
	public float scanIntervalTime = 0;

	void Start () 
	{
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
        source = GetComponent <AudioSource> ();
		source.clip = Microphone.Start (null, true, 10, 44100);
        if (source.clip == null) {
            Debug.LogError("Could not start microphone");
            enabled = false;
            return;
        }
		source.loop = true;
		source.mute = false;
		while (!(Microphone.GetPosition(null) > 0)) {} // Wait until the recording has started
		source.Play ();

	}

	public float GetAveragedVolume()
	{ 
		float[] data = new float[1024];
		float sum = 0;
		source.GetOutputData(data,0);
		foreach(float s in data)
		{
			sum += s;
		}
		float avg = sum / 1024;

		sum = 0;
		foreach(float s in data)
		{
			sum += Mathf.Pow(avg - s, 2f);
		}

		sum = Mathf.Sqrt (sum);
		sum = sum / 256;
		return sum;
	}


}

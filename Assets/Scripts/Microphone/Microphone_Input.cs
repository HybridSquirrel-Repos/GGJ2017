using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Microphone_Input : MonoBehaviour {
	public float requiredScanVolume = 0.2f;
	public float scanInterval = 5;
	AudioSource source;
	public float scanIntervalTime = 0;

	// Use this for initialization
	void Start () 
	{
		source = GetComponent <AudioSource> ();
		source.clip = Microphone.Start (null, true, 10, 44100);
		source.loop = true;
		source.mute = false;
		while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
		source.Play ();

	}

	void Update()
	{
	
	}

	public float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		source.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256;
	}


}

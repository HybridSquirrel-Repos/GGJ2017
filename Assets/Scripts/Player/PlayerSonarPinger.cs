using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSonarPinger : MonoBehaviour
{

    public GameObject sonarPointPrefab;

    public int pointCount;

    Microphone_Input mic;

	static Text debugVolumeText;

    public static void sonar(GameObject sonarPointPrefab, int count, float volume = 10f)
    {
        sonar(sonarPointPrefab, Camera.main.transform, count, volume);
    }

    public static void sonar(GameObject sonarPointPrefab, Transform source, int count, float volume = 10f)
    {

        //Debug.Log (count);

        for (var i = 0; i < count; i++)
        {
            var ray = new Ray(source.position, source.forward * 0.1f + (Random.insideUnitSphere * 0.13f));
            Sonar.ShootRay(ray, sonarPointPrefab, volume);
        }

        if (volume > 3)
            new Noise(source.position, volume);
    }

	// Use this for initialization
	void Start () {
		mic = gameObject.transform.parent.GetComponentInChildren<Microphone_Input> ();
		debugVolumeText = GameObject.Find ("volumeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		var volume = mic.GetAveragedVolume ();
		if (volume > 0.02f) {
			volume = volume * 200f;
			volume = Mathf.Clamp (volume, volume, 30f);
			sonar (sonarPointPrefab, (int)(volume*2), volume);
			debugVolumeText.text = volume.ToString ();
		}
			



        if (Input.GetMouseButton(0))
        {
            sonar(sonarPointPrefab, (int)(3000f * Time.deltaTime), 25f);
        }
		if (Input.GetMouseButton (1)) {
			sonar (sonarPointPrefab, (int)(500f*Time.deltaTime),15f);
		}
		if (Input.GetKeyDown (KeyCode.Y))
		{
			sonar (sonarPointPrefab, 1);
		}
	}
}
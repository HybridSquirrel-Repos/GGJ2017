using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class disturbs a bot.
/// </summary>
public class PlayerBotDisturber : MonoBehaviour {

    public double radius_of_detecion = 4.5f;
    public double effectivity = 50f;

    public static List<BotAI> ai_list = new List<BotAI>();

	void Update ()
    {
        foreach (var ai in ai_list) {
            float dist = Vector3.Distance(ai.transform.position, this.transform.position);
            if (dist < radius_of_detecion) {
                ai.Disturb(effectivity * Time.deltaTime / dist);
            }
        }
	}
}

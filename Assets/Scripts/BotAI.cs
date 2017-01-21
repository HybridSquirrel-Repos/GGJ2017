#define TESTING

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent))]
public class BotAI : MonoBehaviour {

    public enum GuardBehaviour {
        STILL,
        AREA,
        POINT2POINT
    }

    public Transform[] position_points;
    public int points_index = 0;
    public GuardBehaviour guard_behaviour = GuardBehaviour.STILL;
    public double time_until_next_point = 3f;
    public List<Noise> heard_noises = new List<Noise>();

    // Is > 1 if the bot is chasing a player. //
    public double chasing = 0;

    // The amount of awarness the bot has for the player. //
    [Range(0, 100)]
    public double awareness_of_player = 0;

    // How long the entity will be standing still. //
    public double temporary_idle = 0;

    public double chase_determination = 7f;

    // Caches & private data. //
    private Rigidbody rigidbody;
    private GameObject player;
    private NavMeshAgent agent;
	private Vector3 area_destination = new Vector3(-1, -1, -1);

    /// <summary>
    ///  Used to detect changes in behaviour.
    /// </summary>
    private GuardBehaviour old_behaviour = GuardBehaviour.STILL;

	/// <summary>
	/// If we are waiting to move to the next position (AREA only)
	/// </summary>
	private bool waiting = false;

    /// <summary>
    /// Get components and player target.
    /// </summary>
    void Start()
    {

        PlayerBotDisturber.ai_list.Add(this);
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

#if TESTING
        if (player == null)
            player = GameObject.Find("GAME_Player");

#endif
    }

    void OnDrawGizmos()
    {
        if (chasing > 0) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(player.transform.position, 0.5f);
        }
		foreach (Transform point in position_points)
		{
        	Gizmos.color = Color.blue;
        	Gizmos.DrawSphere(point.position, 0.5f);
		}
		if (guard_behaviour == GuardBehaviour.AREA)
		{
			Vector3 pos1 = position_points [0].position;
			Vector3 pos2 = position_points [1].position;
			Vector3 center = Vector3.Lerp(position_points [0].transform.position, position_points [1].transform.position, 0.5f);
			center.y = 0.001f;
			Vector3 size = new Vector3 (Mathf.Abs (pos1.x - pos2.x), 0.01f, Mathf.Abs (pos1.z - pos2.z));
			Gizmos.color = new Color (1, 0, 1, 0.5f);
			Gizmos.DrawCube (center, size);

			Gizmos.color = Color.red;
			Gizmos.DrawSphere (area_destination, 0.5f);
		}
    }

    void DoMovement()
    {
        if (temporary_idle > 0.0f) {
            temporary_idle -= Time.deltaTime;
            agent.enabled = false;
            old_behaviour = GuardBehaviour.STILL;
			waiting = false;
            return;
        }
        agent.enabled = true;

        if (chasing > 0) {
            agent.SetDestination(player.transform.position);
            chasing -= Time.deltaTime;
        }

        else {
			switch (guard_behaviour) {
			case GuardBehaviour.STILL:
				break;
			case GuardBehaviour.POINT2POINT:
				if (Vector3.Distance (transform.position, position_points [points_index].position) < 1) {
					temporary_idle = time_until_next_point;
					points_index++;
				if (points_index >= position_points.Length)
					points_index = 0;
					old_behaviour = GuardBehaviour.STILL;
				}

				if (old_behaviour != guard_behaviour) {
					old_behaviour = guard_behaviour;
					agent.SetDestination (position_points [points_index].position);
				}

			break;
			case GuardBehaviour.AREA:
				if (temporary_idle <= 0 && waiting)
				{
					GetNewAreaDest ();
					agent.SetDestination (area_destination);

					waiting = false;
				}
				if (Vector3.Distance (transform.position, area_destination) < 0.1f)
				{
					transform.position = area_destination;
					waiting = true;
					temporary_idle = time_until_next_point;

				} else
				{
					if (area_destination == null || area_destination == -Vector3.one)
					{
						GetNewAreaDest ();
						agent.SetDestination (area_destination);

					} else
					{

					}
				}
			break;
			}
		}
    }

    void GetNewAreaDest() {
		area_destination = new Vector3( RandomWithNegative(position_points[0].position.x, position_points[1].position.x), transform.position.y,
										RandomWithNegative(position_points[0].position.z, position_points[1].position.z));
		Debug.Log (area_destination);

    }

	float RandomWithNegative(float a, float b)
	{
		if (a > b)
		{
			return Random.Range (b, a);
		} else if (b > a)
		{
			return Random.Range (a, b);
		} else
		{
			return a;
		}
	}

    /// <summary>
    /// Should be called when a player disturbs the bot.
    /// </summary>
    /// <param name="amount">How much the bot was disturbed (min: 0, max: 100) </param>
    public void Disturb(double amount)
    {
        awareness_of_player += amount;
    }

    /*void TryFollowNoise()
    {
        Noise most_important
        foreach (var noise in heard_noises) {
            noise.CalcPriority(transform.position);
        }
    }*/

	void Update ()
    {
        if (awareness_of_player >= 100) {
            chasing = chase_determination;
            awareness_of_player = 0;
            temporary_idle = 0;
        }
        DoMovement();

        if (Random.Range(0.1f, 1f) > 0.2f) {
            TryFollowNoise();
        }
	}
}

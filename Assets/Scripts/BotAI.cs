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
    private Noise goto_noise = null;
    private int goto_noise_key;

	/// <summary>
	/// If we are waiting to move to the next position (AREA only)
	/// </summary>
	public bool waiting = false;

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

        foreach (Transform point in position_points)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(point.position, 0.5f);
        }
        if (goto_noise != null)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(goto_noise.origin, 0.5f);
        }

        if (chasing > 0) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(player.transform.position, 0.5f);
        }
		if (guard_behaviour == GuardBehaviour.AREA)
		{
			Vector3 pos1 = position_points [0].position;
			Vector3 pos2 = position_points [1].position;
			Vector3 center = Vector3.Lerp(position_points [0].transform.position, position_points [1].transform.position, 0.5f);
            center.y = pos1.y;
			Vector3 size = new Vector3 (Mathf.Abs (pos1.x - pos2.x), 0.01f, Mathf.Abs (pos1.z - pos2.z));
			Gizmos.color = new Color (1, 0, 1, 0.5f);
			Gizmos.DrawCube (center, size);

            if (area_destination != -Vector3.one)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(area_destination, 0.5f);
            }
		}

        Gizmos.color = Color.black;
        foreach (var noise in heard_noises)
        {
            if (noise == null)
                continue;
            Gizmos.DrawSphere(noise.origin, 0.25f);
        }
    }

    void DoMovement()
    {
        if (temporary_idle > 0.0f) {
            temporary_idle -= Time.deltaTime;
            agent.enabled = false;
			waiting = false;
            return;
        }
        agent.enabled = true;

        if (goto_noise != null) {
            agent.SetDestination(heard_noises[goto_noise_key].origin);
            agent.SetDestination(goto_noise.origin);
            Debug.Log("GOTO:ing");
            waiting = true;
            if (Vector3.Distance(transform.position, goto_noise.origin) < 1f)
            {
                heard_noises.RemoveAt(goto_noise_key);
                goto_noise = null;
            }
            else
            {
                return;
            }

        }

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
                        waiting = true;
                    }
				agent.SetDestination (position_points [points_index].position);

			break;
			case GuardBehaviour.AREA:
				if (temporary_idle <= 0 && waiting)
				{
					GetNewAreaDest ();
					agent.SetDestination (area_destination);

					waiting = false;
                    temporary_idle = 0;
                    return;
				}

                if (area_destination == -Vector3.one)
                {
                    GetNewAreaDest();
                    agent.SetDestination(area_destination);

                    waiting = false;
                }

                else if (Vector3.Distance (transform.position, area_destination) < 0.1f)
				{
					waiting = true;
					temporary_idle = time_until_next_point;
                    area_destination = -Vector3.one;

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

    void TryFollowNoise()
    {

        float latest_p = 0;
        int count = 0;
        foreach (var noise in heard_noises) {  
            //if (noise )
            float p = noise.CalcPriority(transform.position);
            if (p > latest_p)
            {
                goto_noise = noise;
                goto_noise_key = count;
                latest_p = p;
            }
            count++;

        }
        //if (goto_noise != null)
            //agent.SetDestination(goto_noise.origin);
    }

	void Update ()
    {
        if (awareness_of_player >= 100) {
            chasing = chase_determination;
            awareness_of_player = 0;
            temporary_idle = 0;
        }
        if (goto_noise == null)
        {
            if (Random.Range(0.1f, 1f) > 0.2f)
            {
                TryFollowNoise();
            }
        }


        DoMovement();

        
        if (goto_noise != null)
            Debug.Log(goto_noise.origin);
	}
}

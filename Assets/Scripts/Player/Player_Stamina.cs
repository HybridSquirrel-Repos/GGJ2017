using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Player_Stamina : MonoBehaviour 
{
	/// <summary>
	/// The maximum amount of stamina.
	/// </summary>
	public float maxStamina = 3;

	/// <summary>
	/// If we are not walking, how fast the stamina should regain (secs * staminaRegainmultiplier)
	/// </summary>
	public float staminaRegainMultiplier = 1.2f;

	/// <summary>
	/// Reference to first person controller
	/// </summary>
	public FirstPersonController fpc;


	private float currentStamina = 0;

	private float exhaustCountDown = -10;


	// Update is called once per frame
	void Update () 
	{

		/* If we are not walking, then we are running */
		if (!fpc.m_IsWalking)
		{
			/* Decay the stamina */
			currentStamina -= Time.deltaTime;

			/* Bottom cap the stamina to 0 */
			if (currentStamina < 0)
			{
				currentStamina = 0;
			}
		} else
		{
			/* Else we are walking therefore increase te stamina */
			currentStamina += staminaRegainMultiplier * Time.deltaTime;

			/* Cap the stamina */
			if (currentStamina > maxStamina)
			{
				currentStamina = maxStamina;
			}
		}
		if (exhaustCountDown > 0)
		{
			exhaustCountDown -= Time.deltaTime;
		}

	}

	void LateUpdate()
	{
		if (currentStamina <= 0)
		{
			fpc.m_CanRun = false;
			if (exhaustCountDown == -10)
			{
				exhaustCountDown = 3;
			}
		} else
		{
			if (exhaustCountDown <= 0)
			{
				fpc.m_CanRun = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

/*
 * This script rotates the players camera
 * and transform based on the mouse movement.
 * The entire model is rotated in the yaw direction,
 * but when looking up and down only the camear is rotated.
*/

public class Player_Look : MonoBehaviour 
{

	// the speed we look at
	[SerializeField] private float lookSpeed = 100;

	// the camera component, for rotating in the pitch angle (x)
	[SerializeField] private Camera cam;

	// the minimum and maximum pitch rotation
	[SerializeField] private float minPitch;
	[SerializeField] private float maxPitch;


	// Update is called once per frame
	void Update () 
	{
		// get the vector at which we should look at
		float horLook = Input.GetAxis ("Mouse X") * lookSpeed * Time.deltaTime;
		float verLook = Input.GetAxis ("Mouse Y") * lookSpeed * Time.deltaTime;

		// rotate transform in the yaw direction (y)
		transform.Rotate (0, horLook, 0);
		// rotate camera in pitch direction (x)

		cam.transform.Rotate (-verLook, 0, 0);
	}

	bool CanRotatePitch (float minPitch, float maxPitch)
	{
		float pitch = transform.rotation.eulerAngles.x;

		return pitch >= minPitch && pitch <= maxPitch;
	}

	float ClampPitch (float pitch, float minPitch, float maxPitch)
	{
		if (pitch > 360)
		{
			pitch -= 360;
		} else if (pitch < 360)
		{
			pitch += 360;
		}

		return Mathf.Clamp (pitch, minPitch, maxPitch);


	}


}

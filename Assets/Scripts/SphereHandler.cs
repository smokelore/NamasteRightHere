using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereHandler : MonoBehaviour
{
	public static SphereHandler instance;
	public GameObject Handle;
	public GameObject Target;

	void Awake ()
	{
		instance = this;
	}
	
	void Update ()
	{
		Quaternion newRot = Quaternion.Inverse(Input.gyro.attitude);
		newRot = Quaternion.Slerp(this.transform.rotation, newRot, 5 * Time.deltaTime);
		this.transform.rotation = newRot;
	}

	public void NextTargetPosition()
	{
		Target.transform.position = CalculateNewTargetPosition();
	}

	public Vector3 CalculateNewTargetPosition()
	{
		// TEMPORARY HACK, MAKE DEPENDENT ON PREVIOUS POSITIONS TOO
		Vector3 newPos = Random.onUnitSphere;

		return newPos;
	}
}

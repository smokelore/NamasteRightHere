using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SphereHandler : MonoBehaviour
{
	public static SphereHandler instance;
	public HandleHandler Handle;
	public GameObject Target;

	void Awake ()
	{
		instance = this;
	}
	
	void Update ()
	{
		UpdateRotation();
	}

	void UpdateRotation()
	{
		Quaternion newRot = Quaternion.Inverse(Input.gyro.attitude);
		newRot = Quaternion.Slerp(this.transform.rotation, newRot, 5 * Time.deltaTime);
		this.transform.rotation = newRot;
	}

	public void SetNextTargetPosition()
	{
		Target.transform.position = CalculateNewTargetPosition();
	}

	public Vector3 CalculateNewTargetPosition()
	{
		// TEMPORARY HACK, MAKE DEPENDENT ON PREVIOUS POSITIONS TOO
		Vector3 randomDirection = Random.onUnitSphere;
		Vector3 toHandle = Handle.transform.position - this.transform.position;
		Vector3 alterDirection = randomDirection - Vector3.Dot(toHandle.normalized, randomDirection) * toHandle.normalized;
		alterDirection = alterDirection.normalized;
		float alterAmount = (0.3f + Random.Range(0.0f, 0.5f));

		Vector3 newPos = (Handle.transform.position + alterDirection * alterAmount).normalized;

		Handle.hitTimer = 0f;

		return newPos;
	}
}

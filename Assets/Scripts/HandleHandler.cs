using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandleHandler : MonoBehaviour
{
	public GameObject Target;
	public bool hittingTarget;
	public float hitTimer;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (CollidingWithTarget())
		{
			hitTimer += Time.deltaTime;
		}
		else
		{
			hitTimer = 0f;
		}
	}

	bool CollidingWithTarget()
	{
		if (GetDistanceFromTarget() <= Target.transform.lossyScale.x/2f)
		{
			hittingTarget = true;
			return true;
		}
		else
		{
			hittingTarget = false;
			return false;
		}
	}

	public float GetDistanceFromTarget()
	{
		return Vector3.Distance(this.transform.position, Target.transform.position);
	}

	public Vector3 GetVector3ToTarget()
	{
		return Target.transform.position - this.transform.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		Collider otherCollider = collision.collider;
		if (collider.gameObject == Target)
		{
			// Hitting the target
			hittingTarget = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		Collider otherCollider = collision.collider;
		if (collider.gameObject == Target)
		{
			// No longer hitting the target
			hittingTarget = false;
			hitTimer = 0f;
		}
	}
}

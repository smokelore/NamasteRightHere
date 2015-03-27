using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraHandler : MonoBehaviour
{
	public GameObject Sphere;

	void Start ()
	{
		Input.gyro.enabled = true;
	}
	
	void Update ()
	{
		UpdatePosition();
		UpdateRotation();
	}

	void UpdatePosition()
	{
		this.transform.position = Input.gyro.attitude.eulerAngles.normalized * 2f;
	}

	void UpdateRotation()
	{
		this.transform.LookAt(Sphere.transform.position);
	}
}

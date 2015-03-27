using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundHandler : MonoBehaviour
{
	public static SoundHandler instance;
	public AudioSource sineX, sineY, sineZ;

	void Awake ()
	{
		instance = this;
	}
	
	void Update ()
	{
	
	}

	public void AlterFrequenciesByVector3(Vector3 amount)
	{
		sineX.pitch = 1f + 0.15f * amount.x;
		sineY.pitch = 1f + 0.15f * amount.y;
		sineZ.pitch = 1f + 0.15f * amount.z;

		sineX.volume = 1f - Mathf.Pow(amount.magnitude/2f, 0.2f);
		sineY.volume = 1f - Mathf.Pow(amount.magnitude/2f, 0.2f);
		sineZ.volume = 1f - Mathf.Pow(amount.magnitude/2f, 0.2f);
	}
}

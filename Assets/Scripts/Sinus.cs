using UnityEngine;
using System;  // Needed for Math

public class Sinus : MonoBehaviour
{
	// un-optimized version
	public double[] defaultFrequency = {440.0, 554.365, 659.225f};
	public double[] frequency = {440.0, 554.365, 659.225f};
	public double gain = 0.05/3;

	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	void Start()
	{
		AlterFrequenciesByVector3(Vector3.zero);
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		foreach (double currentFrequency in frequency)
		{
			// update increment in case frequency has changed
			increment = currentFrequency * 2 * Math.PI / sampling_frequency;
			for (var i = 0; i < data.Length; i = i + channels)
			{
				phase = phase + increment;
				// this is where we copy audio data to make them “available” to Unity
				data[i] = (float)(gain*Math.Sin(phase));
				// if we have stereo, we copy the mono data to each channel
				if (channels == 2) data[i + 1] = data[i];
				if (phase > 2 * Math.PI) phase = 0;
			}
		}
	}

	// amount between -1.0 and 1.0, inclusive
	public void AlterFrequency(int index, double amount)
	{
		amount = Mathf.Abs((float)amount);

		if (amount >= 0)
		{
			this.frequency[index] = defaultFrequency[index] + defaultFrequency[index]/2.0 * amount;
		}
		else if (amount < 0)
		{
			this.frequency[index] = defaultFrequency[index] - defaultFrequency[index]/4.0 * amount;
		}

		//this.phase = 0;
	}

	public void AlterFrequenciesByVector3(Vector3 amount)
	{
		AlterFrequency(0, amount.x);
		AlterFrequency(1, amount.y);
		AlterFrequency(2, amount.z); 
	}

	// angle between -180f and 180f, inclusive
	public void AlterFrequencyByAngle(int index, double angle)
	{
		AlterFrequency(index, angle/180.0);
	}
} 


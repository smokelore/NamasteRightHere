﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// This is a sample state manager that uses
public class MainStateManager : MonoBehaviour {
	#region VARIABLES
	// INSTANCE
	public static MainStateManager instance;

	// STATES
	public SimpleStateMachine stateMachine;
	SimpleState menuState, calibrateState, poseState, winState;

	public SphereHandler Sphere;
	public GameObject Target;
	public float hitDuration;

	public GameObject MenuCanvasObject;
	public GameObject CalibrateCanvasObject;
	public Text numSeconds;
	public GameObject PoseCanvasObject;
	public Text numPoints;
	public GameObject WinCanvasObject;
	public Text numScore;

	#endregion

	void Awake () 
	{
		instance = this;
		Input.gyro.enabled = true;

		PoseCanvasObject.SetActive(false);
		CalibrateCanvasObject.SetActive(false);
		WinCanvasObject.SetActive(false);
	}

	void Start () 
	{
		//DEFINE STATES
		menuState = new SimpleState(MenuEnter, MenuUpdate, MenuExit, "[MENU]");
		calibrateState = new SimpleState(CalibrateEnter, CalibrateUpdate, CalibrateExit, "[CALIBRATE]");
		poseState = new SimpleState(PoseEnter, PoseUpdate, PoseExit, "[POSE]");
		winState = new SimpleState(WinEnter, WinUpdate, WinExit, "[WIN]");

		// this is how you switch states!
		stateMachine.SwitchStates(menuState);
	}

	void Update() 
	{
		//Debug.Log("Angle: " + Input.gyro.attitude);
		Execute();
	}

	// This is called every frame. 
	public void Execute () 
	{
		stateMachine.Execute();
	}

	#region MENU
	void MenuEnter() 
	{
		score = 0;
		SoundHandler.instance.Mute(true);
		MenuCanvasObject.SetActive(true);
	}

	void MenuUpdate() 
	{
		if (Input.touchCount > 0)
		{
			stateMachine.SwitchStates(calibrateState);
		}
	}	

	void MenuExit()
	{
		MenuCanvasObject.SetActive(false);
		SoundHandler.instance.Mute(false);
	}
	#endregion

	#region CALIBRATE
	Timer calibrateTimer;

	void CalibrateEnter() 
	{
		Input.gyro.enabled = true;
		CalibrateCanvasObject.SetActive(true);
		calibrateTimer = new Timer(10.0f);
	}

	void CalibrateUpdate() 
	{
		Target.transform.position = (Camera.main.transform.position - Sphere.transform.position).normalized;

		numSeconds.text = "" + (10 - (int)(10 *  	calibrateTimer.Percent()));

		if (calibrateTimer.Percent() >= 1.0f)
		{
			stateMachine.SwitchStates(poseState);
			Handheld.Vibrate();
		}
	}	

	void CalibrateExit()
	{
		CalibrateCanvasObject.SetActive(false);
		Sphere.SetNextTargetPosition();
	}
	#endregion

	#region POSE
	int score = 0;
	Timer poseTimer;

	void PoseEnter() 
	{
		PoseCanvasObject.SetActive(true);
		poseTimer = new Timer(120f);
		numPoints.text = "" + score;
	}

	void PoseUpdate() 
	{
		SoundHandler.instance.AlterFrequenciesByVector3(Sphere.Handle.GetVector3ToTarget());

		if (Sphere.Handle.hitTimer >= hitDuration)
		{
			Sphere.SetNextTargetPosition();
			Handheld.Vibrate();
			score++;
			numPoints.text = "" + score;
		}
		else if (Sphere.Handle.hitTimer > 0f)
		{
			Handheld.Vibrate();
		}

		if (poseTimer.Percent() >= 1f)
		{
			stateMachine.SwitchStates(winState);
		}
	}

	void PoseExit()
	{
		PoseCanvasObject.SetActive(false);
	}
	#endregion

	#region WIN
	Timer winTimer;
	void WinEnter() 
	{
		WinCanvasObject.SetActive(true);
		numScore.text = "" + score;
		winTimer = new Timer(10.0f);
		SoundHandler.instance.Mute(true);
	}

	void WinUpdate() 
	{
		Handheld.Vibrate();

		if (winTimer.Percent() >= 1f)
		{
			stateMachine.SwitchStates(menuState);
		}
	}

	void WinExit() 
	{
		WinCanvasObject.SetActive(false);
	}
	#endregion
}

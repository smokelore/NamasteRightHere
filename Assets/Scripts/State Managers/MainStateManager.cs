using UnityEngine;
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

	public Sinus Sinus;

	#endregion

	void Awake () 
	{
		instance = this;
	}

	void Start () 
	{
		//DEFINE STATES
		menuState = new SimpleState(MenuEnter, MenuUpdate, MenuExit, "[MENU]");
		calibrateState = new SimpleState(CalibrateEnter, CalibrateUpdate, CalibrateExit, "[CALIBRATE]");
		poseState = new SimpleState(PoseEnter, PoseUpdate, PoseExit, "[POSE]");
		winState = new SimpleState(WinEnter, WinUpdate, WinExit, "[WIN]");

		// this is how you switch states!
		stateMachine.SwitchStates(calibrateState);
	}

	void Update() 
	{
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
		
	}

	void MenuUpdate() 
	{
		
	}	

	void MenuExit()
	{
		
	}
	#endregion

	#region CALIBRATE
	Timer calibrateTimer;

	void CalibrateEnter() 
	{
		calibrateTimer = new Timer(10.0f);
	}

	void CalibrateUpdate() 
	{
		Target.transform.position = (Camera.main.transform.position - Sphere.transform.position).normalized;
		if (calibrateTimer.Percent() >= 1.0f)
		{
			stateMachine.SwitchStates(poseState);
			Handheld.Vibrate();
		}
	}	

	void CalibrateExit()
	{
		Sphere.SetNextTargetPosition();
	}
	#endregion

	#region POSE
	int score = 0;

	void PoseEnter() 
	{
		
	}

	void PoseUpdate() 
	{
		Debug.Log("Angle: " + Sphere.Handle.GetVector3ToTarget());
		SoundHandler.instance.AlterFrequenciesByVector3(Sphere.Handle.GetVector3ToTarget());

		if (Sphere.Handle.hitTimer >= hitDuration)
		{
			Sphere.SetNextTargetPosition();
			Handheld.Vibrate();
			score++;
		}

		if (score > 20)
		{
			stateMachine.SwitchStates(winState);
		}
	}

	void PoseExit()
	{

	}
	#endregion

	#region WIN
	void WinEnter() 
	{
		
	}

	void WinUpdate() 
	{
		
	}

	void WinExit() 
	{
		
	}
	#endregion
}

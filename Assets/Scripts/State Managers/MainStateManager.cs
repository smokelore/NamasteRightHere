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

	// Note: it is common to have the SampleStateManager oversee other StateManagers. (calling their StateManager.Execute() methods within this SampleStateManager's appropriate states' update method).
		// For example, if there were a BossStateManager attached to the boss enemy and it was defeated, the BossStateManager might enter the deathState. 
		// This SampleStateManager might see that the BossStateManager was in the deathState (ie. bossStateManager.state == "DEATH") and cause a transition to the winState as a result.
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
	void CalibrateEnter() 
	{

	}

	void CalibrateUpdate() 
	{

	}	

	void CalibrateExit()
	{

	}
	#endregion

	#region POSE
	void PoseEnter() 
	{
		
	}

	void PoseUpdate() 
	{
		
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

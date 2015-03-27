using UnityEngine;
using System.Collections;

[System.Serializable]
public class SimpleStateMachine {
	
	public delegate void StateDelegate();
	private StateDelegate enter, update, exit;
	public string currentState;
	
	//call this in the update function
	public void Execute () {
		if (update != null){
			update();
		}
	}
	
	public void SwitchStates(StateDelegate enter, StateDelegate update, StateDelegate exit, string name){
		string switchDebug = "STATE TRANSITION: ";

		if (this.exit != null){
			switchDebug += this.currentState + " ";
			this.exit(); //exit the current state
		}
		
		//set up the new state
		this.enter = enter;
		this.update = update;
		this.exit = exit;
		this.currentState = name;
		
		if (this.enter != null){
			switchDebug += "--> " + name;
			this.enter(); //enter the new state
		}

		Debug.Log(switchDebug);
	}
	
	public void SwitchStates(SimpleState state) {
		SwitchStates(state.enter, state.update, state.exit, state.name);
	}
	
	public SimpleState GetCurrentState(){
		return new SimpleState(enter, update, exit, currentState);
	}
}

using UnityEngine;
using System.Collections;

[System.Serializable]
public class SimpleState {
	public string name;
	public SimpleStateMachine.StateDelegate enter, update, exit;
	
	public SimpleState(SimpleStateMachine.StateDelegate enter, 
				SimpleStateMachine.StateDelegate update, 
				SimpleStateMachine.StateDelegate exit,
				string name){
		this.enter = enter;
		this.update = update;
		this.exit = exit;
		this.name = name;
	}
}

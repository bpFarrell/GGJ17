using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartResetState()
	{
		GameStateMachine._Main.ChangeState(GameStateMachine.STATE.RETURN);
	}
}

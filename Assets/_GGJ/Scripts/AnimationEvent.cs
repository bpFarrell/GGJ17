using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	bool animFinished = false;

	void Start()
	{

	}

	public void EndUpdate()
	{
		bool inputDetected = Input.GetButtonDown("P1_A") || Input.GetButtonDown("P1_Start") || Input.GetButtonDown("P2_A") || Input.GetButtonDown("P2_Start");

		if(inputDetected && animFinished)
		{
			GameStateMachine._Main.ChangeState(GameStateMachine.STATE.RETURN);
		}
	}

	void StartResetState()
	{
		animFinished = true;
		//GameStateMachine._Main.ChangeState(GameStateMachine.STATE.RETURN);
	}
}

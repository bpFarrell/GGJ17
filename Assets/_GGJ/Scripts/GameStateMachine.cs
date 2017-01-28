using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour {
	
	static public GameStateMachine _MAIN;
	
	public enum STATE 
	{
		START   = 0,
		MENU    = 1,
		LOADING = 2,
		GAME	= 3,
		END		= 4,
		RETURN	= 5,
		NULL    = 6
	}
	
	public STATE eGameState = STATE.NULL;
	
	public delegate void StateDelegate();
	
	public class genericState
	{
		public genericState()
		{
			StartOfState = delegate {};
			UpdateState = delegate {};
			ExitState = delegate {};
		}
		public StateDelegate StartOfState;
		public StateDelegate UpdateState;
		public StateDelegate ExitState;
	}
	
	public genericState sStart   = new genericState();
	public genericState sMenu    = new genericState();
	public genericState sLoading = new genericState();
	public genericState sGame	 = new genericState();
	public genericState sEnd	 = new genericState();
	public genericState sReturn	 = new genericState();
	public genericState sNull	 = new genericState();

	private genericState[] stateContainer;
	
	private StateDelegate updateDelegate = delegate {};
	
	// Use this for initialization
	void Start ()
	{
		if (GameStateMachine._MAIN == null)
			GameStateMachine._MAIN = this;
		else
		{
			Debug.LogError("Multiple GameStateMachine exist in scene: Deleting extra instance.");
			Destroy (this);
		}
		
		stateContainer = new genericState[] {sMenu, sMenu, sLoading, sGame, sEnd, sReturn, sNull};
		updateDelegate = delegate {
			ChangeState(STATE.START);
		};
	}
	
	public void ChangeState(STATE state)
	{
		if (state == null) 
		{
			Debug.LogError("ChangeState in GameStateController was given null parameter.");
			return;
		}
		if(state == eGameState)
			return;
		if (state == STATE.NULL)
		{
			Debug.Log ("GameState is entering NULL state.");
		}
		
		stateContainer[((int) eGameState)].ExitState();
		eGameState = state;
		stateContainer[((int) eGameState)].StartOfState();
		updateDelegate = stateContainer[((int) eGameState)].UpdateState;
	}
	
	// Update is called once per frame
	void Update () 
	{
		updateDelegate();
	}
}

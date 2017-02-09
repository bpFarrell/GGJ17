using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	static public GameStateManager _MAIN;
	
	public enum STATE 
	{
		NULL  = 0,
		INIT  = 1,
		PLAY  = 2,
		END   = 3,
		RESET = 4
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
	
	public genericState NullState = new genericState();
	public genericState Init = new genericState();
	public genericState Play = new genericState();
	public genericState End = new genericState();
	public genericState Reset = new genericState();
	
	private genericState[] stateContainer;
	
	private StateDelegate updateDelegate = delegate {};
	
	// Use this for initialization
	void Awake ()
	{
		if (GameStateManager._MAIN == null)
			GameStateManager._MAIN = this;
		else
		{
			Debug.LogError("Multiple GameStateManager exist in scene: Deleting extra instance.");
			Destroy (this);
		}
		
		stateContainer = new genericState[] {NullState, Init, Play, End, Reset};
	}
	
	public void ChangeState(STATE state)
	{
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
		/*switch (eGameState)
		{
		default:
			break;
		case STATE.INIT:
			InitDelegate();
			break;
		case STATE.PLAY:
			PlayDelegate();
			break;
		case STATE.END:
			EndDelegate();
			break;
		case STATE.RESET:
			ResetDelegate();
			break;
		}*/
	}
}

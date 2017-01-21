using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour 
{

	static public GameManagerScript _MAIN;

	public enum STATE 
	{
		INIT  = 0,
		PLAY  = 1,
		END   = 2,
		RESET = 3
	}

	public STATE eGameState = STATE.INIT;

	public delegate void StateDelegate();

	public class genericState
	{
		public bool Initializated = false;
		public STATE type;
		public genericState(STATE enu)
		{
			type = enu;
		}

		public StateDelegate StartOfState = delegate {};
		public StateDelegate UpdateState = delegate {};
		public StateDelegate ExitState = delegate {};
	}

	public genericState Init;
	public genericState Play;
	public genericState End;
	public genericState Reset;

	private genericState[] stateContainer;

	private StateDelegate updateDelegate = delegate {};

	// Use this for initialization
	void Start ()
	{
		if (GameManagerScript._MAIN == null)
			GameManagerScript._MAIN = this;
		else
		{
			Debug.LogError("Multiple GameManagerScript exist in scene: Deleting extra instance.");
			Destroy (this);
		}

		stateContainer = new genericState[] {Init, Play, End, Reset};
	}

	void ChangeState(STATE state)
	{
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

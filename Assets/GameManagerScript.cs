using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour 
{
	static public GameManagerScript _MAIN;
	
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

		GameStateManager._MAIN.NullState.StartOfState += CreatePlayers;
		GameStateManager._MAIN.NullState.StartOfState += CreateScorekeeper;
		GameStateManager._MAIN.NullState.StartOfState += CreateFishManager;

		GameStateManager._MAIN.ChangeState(GameStateManager.STATE.NULL);
	}

	// Update is called once per frame
	void Update () 
	{

	}
	
	public void CreatePlayers()
	{
		Debug.Log ("Players Initialized");
		return;
	}

	public void CreateScorekeeper()
	{
		Debug.Log ("Scorekeeper Initialized");
		return;
	}

	public void CreateFishManager()
	{
		Debug.Log ("FishManager Initialized");
		return;
	}
}

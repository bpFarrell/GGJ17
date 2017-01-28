using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour 
{
	static public GameManagerScript _MAIN;

	public GameObject cinematicGroup;
	public GameObject dummyRed;
	public GameObject dummyBlue;

	public Material[] playerMaterials;
	
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

		//GameStateManager._MAIN.NullState.StartOfState += CreatePlayers;
		//GameStateManager._MAIN.NullState.StartOfState += CreateScorekeeper;
		//GameStateManager._MAIN.NullState.StartOfState += CreateFishManager;

		GameStateManager._MAIN.ChangeState(GameStateManager.STATE.NULL);
		GameStateManager._MAIN.End.StartOfState += InstantiateEndGame;
	}

	void Awake()
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("_Arena"));
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

	public void InstantiateEndGame()
	{
		cinematicGroup.SetActive(true);
		if(GameInfo.victor == 1)
		{
			dummyBlue.SetActive(true);
		}
		else
		{
			dummyRed.SetActive(true);
		}	

		PlayerController.locked = true;

		// count score
	}
}

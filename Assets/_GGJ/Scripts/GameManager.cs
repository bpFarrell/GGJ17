using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	static public GameManager _Main;
	public GameObject dualDisplay;
	public GameObject singleDisplay;
	public GameObject MenuDoodads;
	public GameObject singlePlayerScreen;
	public GameObject TwoPlayerScreen;
	public List<GameObject> LevelGroups = new List<GameObject>();
	public bool ForceDualScreen = false;
	public delegate void simpleDelegate();

	[ReadOnly][SerializeField] private int DisplayCount;
	[ReadOnly][SerializeField] private GameObject DisplayRoot;
	[ReadOnly][SerializeField] private GameObject menuObject;
	[ReadOnly][SerializeField] private GameObject levelObject;
	[ReadOnly][SerializeField] private GameObject ScreenType;
	[ReadOnly][SerializeField] private GameObject CinematicGroup;
	//[ReadOnly][SerializeField] private GameObject PlayButton;
	//[ReadOnly][SerializeField] private GameObject QuitButton;
	[ReadOnly][SerializeField] private GameStateMachine gsm;

	// Use this for initialization
	void Awake ()
	{
		if (!GameManager._Main)
			_Main = this;
		else
		{
			Debug.LogError("Multiple GameManager scripts have appeared in the scene.", gameObject);
			Destroy(this);
		}
		
		DisplayCount = 1;

		Debug.Log("Displays connected: " + Display.displays.Length);
		if(Display.displays.Length > 1)
		{
			Display.displays[1].Activate();
			DisplayCount = 2;
		}
	}
	void Start ()
	{
		gsm = GameStateMachine._Main;
		
		gsm.sStart.StartOfState += GameSetup;
		gsm.sStart.StartOfState += () => {
			menuObject = Instantiate(MenuDoodads) as GameObject;
		};

		gsm.sGame.StartOfState += () => {
			Destroy (menuObject);
		};

		gsm.sLoading.StartOfState += LoadLevel;
		gsm.sEnd.ExitState += UnLoadLevel;
		gsm.sLoading.ExitState += LoadPlayers;
		gsm.sEnd.StartOfState += InstantiateEndGame;
	}

	private void GameSetup()
	{
		Transform titleObj;
		if(DisplayCount > 1 || ForceDualScreen)
		{
			DisplayRoot = Instantiate( dualDisplay ) as GameObject;
			titleObj = DisplayRoot.transform.Find("Screen One").Find("UI Canvas").Find("TitleScreen");
			ScreenType = TwoPlayerScreen;
		}
		else
		{
			DisplayRoot = Instantiate( singleDisplay ) as GameObject;
			titleObj = DisplayRoot.transform.Find("UI Canvas").Find("TitleScreen");
			ScreenType = singlePlayerScreen;
		}
		//titleObj = DisplayRoot.transform.Find("Screen One").Find("UI Canvas").Find("TitleScreen");
		//PlayButton = titleObj.Find("PLAY BUTTON").gameObject;
		//PlayButton.GetComponent<Button>().onClick.AddListener(MenuController._Main.PressedPlay);
		//QuitButton = titleObj.Find("QUIT BUTTON").gameObject;
		//QuitButton.GetComponent<Button>().onClick.AddListener(MenuController._Main.PressedQuit);
	}

	public void LoadLevel()
	{
		levelObject = new GameObject("Level");
		foreach (GameObject obj in LevelGroups) 
		{
			levelObject = Instantiate (obj, levelObject.transform) as GameObject;
		}
	}
	public void LoadPlayers()
	{
		Instantiate (ScreenType, levelObject.transform);
	}

	public void UnLoadLevel()
	{
		Destroy (levelObject);
	}
	public void InstantiateEndGame()
	{
		CinematicGroup = (Resources.FindObjectsOfTypeAll(typeof(Targetable))[0] as Targetable).gameObject;
		CinematicGroup.SetActive(true);
		if(GameInfo.victor == 1)
		{
			//dummyBlue.SetActive(true);
		}
		else
		{
			//dummyRed.SetActive(true);
		}	
		
		PlayerController.locked = true;
		
		// count score
	}
}








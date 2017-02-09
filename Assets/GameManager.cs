using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	static public GameManager _Main;
	public GameObject dualDisplay;
	public GameObject singleDisplay;
	public delegate void simpleDelegate();

	[ReadOnly][SerializeField] private int DisplayCount;
	[ReadOnly][SerializeField] private GameObject DisplayRoot;
	[ReadOnly][SerializeField] private GameObject PlayButton;
	[ReadOnly][SerializeField] private GameObject QuitButton;
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
	}

	private void GameSetup()
	{
		Transform titleObj;
		if(DisplayCount > 1)
		{
			DisplayRoot = Instantiate( dualDisplay ) as GameObject;
			titleObj = DisplayRoot.transform.Find("Screen One").Find("UI Canvas").Find("TitleScreen");
		}
		else
		{
			DisplayRoot = Instantiate( singleDisplay ) as GameObject;
			titleObj = DisplayRoot.transform.Find("UI Canvas").Find("TitleScreen");
		}

		//titleObj = DisplayRoot.transform.Find("Screen One").Find("UI Canvas").Find("TitleScreen");
		PlayButton = titleObj.Find("PLAY BUTTON").gameObject;
		PlayButton.GetComponent<Button>().onClick.AddListener(MenuController._Main.PressedPlay);
		QuitButton = titleObj.Find("QUIT BUTTON").gameObject;
		QuitButton.GetComponent<Button>().onClick.AddListener(MenuController._Main.PressedQuit);
	}

	private void LoadingScreenStart()
	{
		
	}
}








using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	static public GameManager _Main;

	public int? displaysConnected;

	

	// Use this for initialization
	void Start () 
	{
		if (!GameManager._Main)
			_Main = this;
		else
		{
			Debug.LogError("Multiple GameManager scripts have appeared in the scene.", gameObject);
			Destroy(this);
		}

		transform
		
		Debug.Log("Displays connected: " + Display.displays.Length);
		if(Display.displays.Length > 1)
		{
			Display.displays[1].Activate();
		}
	}
}

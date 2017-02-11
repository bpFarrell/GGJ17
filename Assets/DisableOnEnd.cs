using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEnd : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		GameStateMachine._Main.sEnd.StartOfState += () => gameObject.SetActive(false);
	}
}

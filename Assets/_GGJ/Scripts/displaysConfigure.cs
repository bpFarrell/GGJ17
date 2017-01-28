using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displaysConfigure : MonoBehaviour {

	public bool ran = false;
	// Use this for initialization
	void Start () {
		Debug.Log("Displays connected: " + Display.displays.Length);
		if(Display.displays.Length > 1)
			Display.displays[1].Activate();
		ran = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

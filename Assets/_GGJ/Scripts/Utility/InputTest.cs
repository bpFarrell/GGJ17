using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {
	private string playerPrefix = "P2_";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis(playerPrefix + "LeftHorizontal") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "LeftHorizontal") > 0.0f) Debug.Log("Left Stick tilted right."); 
			if(Input.GetAxis(playerPrefix + "LeftHorizontal") < 0.0f) Debug.Log("Left Stick tilted left.");
		}
		if (Input.GetAxis(playerPrefix + "LeftVertical") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "LeftVertical") > 0.0f) Debug.Log("Left Stick tilted down."); 
			if(Input.GetAxis(playerPrefix + "LeftVertical") < 0.0f) Debug.Log("Left Stick tilted up.");
		}
		if (Input.GetAxis(playerPrefix + "RightHorizontal") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "RightHorizontal") > 0.0f) Debug.Log("Right Stick tilted right."); 
			if(Input.GetAxis(playerPrefix + "RightHorizontal") < 0.0f) Debug.Log("Right Stick tilted left.");
		}
		if (Input.GetAxis(playerPrefix + "RightVertical") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "RightVertical") > 0.0f) Debug.Log("Right Stick tilted down."); 
			if(Input.GetAxis(playerPrefix + "RightVertical") < 0.0f) Debug.Log("Right Stick tilted up.");
		}
		if (Input.GetButtonDown(playerPrefix + "A")) {
			Debug.Log(playerPrefix + "A was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "B")) {
			Debug.Log(playerPrefix + "B was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "Y")) {
			Debug.Log(playerPrefix + "Y was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "X")) {
			Debug.Log(playerPrefix + "X was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "Start")) {
			Debug.Log(playerPrefix + "Start was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "Select")) {
			Debug.Log(playerPrefix + "Select was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "RightStickDown")) {
			Debug.Log(playerPrefix + "RightStickDown was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "LeftStickDown")) {
			Debug.Log(playerPrefix + "LeftStickDown was pressed.");
		}
		if (Input.GetAxis(playerPrefix + "Dpad_Horizontal") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "Dpad_Horizontal") > 0.0f) Debug.Log("Right Dpad Pressed."); 
			if(Input.GetAxis(playerPrefix + "Dpad_Horizontal") < 0.0f) Debug.Log("Left Dpad Pressed.");
		}
		if (Input.GetAxis(playerPrefix + "Dpad_Vertical") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "Dpad_Vertical") > 0.0f) Debug.Log("Up Dpad Pressed."); 
			if(Input.GetAxis(playerPrefix + "Dpad_Vertical") < 0.0f) Debug.Log("Down Dpad Pressed.");
		}
		if (Input.GetAxis(playerPrefix + "Triggers") != 0.0f) {
			if(Input.GetAxis(playerPrefix + "Triggers") > 0.0f) Debug.Log("Left Trigger was pressed.");
			if(Input.GetAxis(playerPrefix + "Triggers") < 0.0f) Debug.Log("Right Trigger was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "LB")) {
			Debug.Log(playerPrefix + "LB was pressed.");
		}
		if (Input.GetButtonDown(playerPrefix + "RB")) {
			Debug.Log(playerPrefix + "RB was pressed.");
		}
	}
}

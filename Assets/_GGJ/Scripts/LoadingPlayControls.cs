using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlayControls : MonoBehaviour 
{

	public string player = "1";
	public float radiusOfMotion = 1.0f;

	private string playerPrefix = "P1_";

	// Use this for initialization
	void Start () 
	{
		playerPrefix = "P" + player + "_";
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 inputVector = new Vector3(Input.GetAxis(playerPrefix + "LeftHorizontal"), Input.GetAxis(playerPrefix + "LeftVertical"), 0.0f);
		transform.localPosition = inputVector.normalized * (radiusOfMotion * inputVector.sqrMagnitude);
	}
}

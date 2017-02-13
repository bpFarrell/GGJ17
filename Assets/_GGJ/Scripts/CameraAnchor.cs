using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour 
{
	public string player = "1";
	public Transform cameraPosition;
	public Vector3 startPosition;
	public float heightAdjSpeed = 1.0f;
	public float height = 1.0f;
	public float roofBound = 15.0f;
	public float floorBound = 5.0f;
	public float chaseSpeed = 0.1f;
	public float rotateSpeed = 1.0f;
	public float targetDistance = 50.0f;
	public cameraMovement cameraScript;

	private string playerPrefix = "P1_";
	// Use this for initialization
	void Start () 
	{
		cameraPosition.position = startPosition;
		playerPrefix = "P" + player + "_";
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool inputDetected = false;
		// Jason Input for camera rotation//
		float horizontalVector = Input.GetAxis(playerPrefix + "RightHorizontal");
		float verticalVector = Input.GetAxis (playerPrefix + "RightVertical");
		//Debug.Log(verticalVector);
		if(horizontalVector >= 0.1f || horizontalVector <= -0.1f) 
		{
			inputDetected = true;
			Vector3 point = (transform.position + Vector3.up);
			Vector3 axis = Vector3.up;
			float rotAngle = Input.GetAxis(playerPrefix + "RightHorizontal") * rotateSpeed * Time.deltaTime * 20;
			cameraPosition.RotateAround(point, axis, rotAngle);
		}
		if (verticalVector >= 0.1f || verticalVector <= -0.1f)
		{
			inputDetected = true;
			height += (heightAdjSpeed * (Input.GetAxis (playerPrefix + "RightVertical"))) * Time.deltaTime;
			if (height > roofBound) {height = roofBound;}
			if (height < floorBound) {height = floorBound;}
		}
		if (inputDetected)
		{
			cameraScript.cameraBroken = true;
		}

		Vector3 tempPos = cameraPosition.position;
		Vector3 displacement = transform.position - cameraPosition.position;
		float step = (displacement.magnitude - targetDistance) * (0.3f * Time.deltaTime);
		tempPos += displacement.normalized * step;
		tempPos.y = transform.position.y + height;
		cameraPosition.position = tempPos;
	}
}

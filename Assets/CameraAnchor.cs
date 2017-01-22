using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour 
{
	public string player = "1";
	public Transform cameraPosition;
	public Vector3 startPosition;
	public float height = 1.0f;
	public float chaseSpeed = 0.1f;
	public float rotateSpeed = 1.0f;
	public float targetDistance = 50.0f;

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
		// Jason Input for camera rotation//
		if(Input.GetAxis(playerPrefix + "RightHorizontal") != 0.0f) 
		{
			Vector3 point = (transform.position + Vector3.up);
			Vector3 axis = Vector3.up;
			float rotAngle = Input.GetAxis(playerPrefix + "RightHorizontal") * rotateSpeed;
			cameraPosition.RotateAround(point, axis, rotAngle);
		}
		Vector3 tempPos = cameraPosition.position;
		Vector3 displacement = transform.position - cameraPosition.position;
		float step = (displacement.magnitude - targetDistance) * chaseSpeed;
		tempPos += displacement.normalized * step;
		tempPos.y = transform.position.y + height;
		cameraPosition.position = tempPos;
	}
}

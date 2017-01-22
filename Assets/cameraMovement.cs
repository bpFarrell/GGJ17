using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour 
{
	public float followSpeed;
	public Transform cameraFollow;
	public Transform lookTarget;

	// Use this for initialization
	void Start () 
	{
		transform.position = cameraFollow.position;
		transform.LookAt(lookTarget.position);
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 displacement = cameraFollow.position - transform.position;
		float step = displacement.magnitude * followSpeed/100;

		Vector3 tempPos = transform.position;
		tempPos += (displacement.normalized * step);

		transform.position = tempPos;
		transform.LookAt(lookTarget.position);
	}
}

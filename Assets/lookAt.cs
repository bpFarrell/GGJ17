using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour {

	public Transform focus;

	// Use this for initialization
	void Start () {
		transform.LookAt(focus.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt(focus.position);
	}
}

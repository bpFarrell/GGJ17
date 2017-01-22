using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tenticleRetract : MonoBehaviour 
{

	private Vector3[] pointCollection;
	// Use this for initialization
	void Start () 
	{
		pointCollection = new Vector3[transform.childCount];
		int i = 0;
		foreach (GameObject child in transform) 
		{
			pointCollection[i] = child.transform.position;
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (GameObject child in transform) 
		{

		}
	}
}

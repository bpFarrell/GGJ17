using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitScript : MonoBehaviour {

	private float _T = 2.0f;
	public float timeToWait
	{
		set
		{
			_T = value;
		}
	}
	public GameManager.simpleDelegate Callback = delegate {};

	[ReadOnly][SerializeField]
	private float counter = 0.0f;

	void Start ()
	{
		counter = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		counter += Time.fixedDeltaTime;
		if (counter >= _T) 
		{
			Callback();
			Destroy(this);
		}
	}
}

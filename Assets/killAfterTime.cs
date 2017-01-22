using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAfterTime : MonoBehaviour 
{
	public float delay = 4.0f;

	private ParticleSystem ps;
	private float counter = 0.0f;
	// Use this for initialization
	void Start () {
		if(ps == null)
			ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if((counter += Time.deltaTime) >= delay)
			ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

		if(!ps.IsAlive())
			Destroy(gameObject);
	}
}

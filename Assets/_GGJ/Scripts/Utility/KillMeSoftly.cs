using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeSoftly : MonoBehaviour {

    public float secondsToDie;
    public bool autoKill;
	// Use this for initialization
	void Start () {

        if (autoKill) {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;
            secondsToDie = main.startLifetime.constantMax + main.duration;
            Debug.Log(secondsToDie);
        }
        Destroy(gameObject, secondsToDie);

	}
	
}

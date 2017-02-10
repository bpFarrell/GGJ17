using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEnable : MonoBehaviour {
    public float delayTime = 1;
    float startTime;
    public GameObject[] gos;
	// Use this for initialization
	void Awake () {
        startTime = Time.time;
	}

	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > startTime + delayTime) {
            TurnOn();
        }
	}
    void TurnOn() {       
        foreach (var go in gos) {
			go.SetActive(true);
		} 
        
        Destroy(this);
    }
}

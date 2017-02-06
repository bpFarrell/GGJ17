using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoLogic : MonoBehaviour {
    public bool ideling=true;
    float speedToIdle = 20;
    float posIdle = -90;
    float speedToActive = 200;
    float posActive = -135;
    public float t = -90;
	void Start () {
		
	}
	void Update () {
        if (ideling) {
            t += speedToIdle * Time.deltaTime;
        }else {
            t -= speedToActive * Time.deltaTime;
            if (t < posActive)
                ideling = true;
        }
        t = Mathf.Clamp(t, posActive, posIdle);
        transform.localRotation = Quaternion.Euler(0, 0, t);
    }
    public void Woosh() {
        ideling = false;
    }
}

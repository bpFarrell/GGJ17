using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeBro : MonoBehaviour {

    public float timeToDie;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDie);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour {
    public GameObject hinge;
    public ParticleSystem[] pes;
    public float maxTurn=-130f;
    public float current = 0;
    public bool opening;

	// Use this for initialization
	void Start () {

        foreach (ParticleSystem pe in pes) {
            pe.Play(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (opening) {
            current -= Time.deltaTime * 100;
            current = Mathf.Max(current, maxTurn);
            hinge.transform.localRotation = Quaternion.Euler(current, 0, 0);
        }
    }
    void OnTriggerEnter(Collider other) {
        opening = true;
        Debug.Log("Hit " + other.gameObject.name);
        foreach(ParticleSystem pe in pes) {
            pe.Play(true);
        }
    }
}

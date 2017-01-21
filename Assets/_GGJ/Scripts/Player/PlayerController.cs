using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector3 velocity = new Vector3();
    public float friction = 1;
    public float power = 5;
    public IList<GameObject> tents = new List<GameObject>();
	// Use this for initialization
	void Start () {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children) {
            if (child.parent = transform) {
                tents.Add(child.gameObject);
            }
        }
        foreach(GameObject go in tents) {
            go.transform.Rotate(-20, 180, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocity += transform.forward*power;
        }


        transform.position += velocity * Time.deltaTime;
        velocity = velocity - (velocity * Time.deltaTime*friction);
	}
}

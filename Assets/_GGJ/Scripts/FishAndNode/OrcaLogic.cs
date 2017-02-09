using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaLogic : MonoBehaviour {
    float speed = 40;
    float deltaY = 12;
    float decayY = 0.5f;
    float killY;
	// Use this for initialization
	void Start () {
        if (Random.value > 0.5f) {
            transform.GetChild(0).localScale *= Random.Range(0.9f, 1.1f);
        }else {
            transform.GetChild(0).localScale *= Random.Range(0.5f, 0.6f);
        }
        killY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.position += transform.up*deltaY*Time.deltaTime;
        deltaY = deltaY - (decayY * Time.deltaTime);
        if (transform.position.y < killY) {
            Destroy(gameObject);
        }
	}
}

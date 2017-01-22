using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swirl : MonoBehaviour {

    public enum State {
        normal,
        recieve,
        flush,
        end
    }
    public State state;

    public float radius = 20;
    public float speed = 20;
//    GameObject swirl;
    GameObject pivot;
    public Transform trackme {
        get {
            return pivot.transform;
        }
    }
	// Use this for initialization
	void Start () {
        pivot = new GameObject("pivot");//GameObject.CreatePrimitive(PrimitiveType.Sphere);// new GameObject("pivot");
        //    swirl = GameObject.CreatePrimitive(PrimitiveType.Cube);// new GameObject("swirl");
        pivot.transform.position = transform.position;
    //    swirl.transform.position = pivot.transform.position + (Vector3.forward * radius);
    //    swirl.transform.SetParent(pivot.transform);
        Debug.Log(pivot.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.normal) {
            pivot.transform.Rotate(0, speed * Time.deltaTime, 0);
            Vector3 pos = pivot.transform.position;
            pivot.transform.position = new Vector3(pos.x, pos.y + (Mathf.Sin(Time.time*0.5f)*0.5f), pos.z);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        RigIt();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RigIt() {
        Transform[] trans = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = trans[i].position + (Vector3.up * 2);
            trans[i].parent = null;
            trans[i].SetParent(go.transform);
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAttack : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        NodeMain nm;
        if ((nm = col.GetComponent<NodeMain>()) != null) {
            Debug.Log("drop it");
            nm.SetToNeutralState();
            
        }
    }
}

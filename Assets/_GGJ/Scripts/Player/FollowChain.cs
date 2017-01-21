using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChain : MonoBehaviour {
    public int length=-1;
    public float distance=1;
    public FollowChain Next;
    public FollowChain Prev;
    void Awake() {
        if (length == -1) return;
        Spawn(null);
    }
    void Update() {
        if (Prev == null) return;
        Vector3 dir = transform.position - Prev.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        if (dist < distance) {
            transform.position = Prev.transform.position + dir * distance;
        }
        transform.position = Vector3.Lerp(transform.position, Prev.transform.position + Prev.transform.forward * 1, Time.deltaTime * 10);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Prev.transform.rotation, Time.deltaTime * 10);
        transform.rotation = Quaternion.LookRotation(dir,transform.up);
        float rDotu = Vector3.Dot(transform.right, Prev.transform.up);
        transform.Rotate(0, 0, -rDotu*Time.deltaTime*200);
    }
    public void Spawn(FollowChain from) {
        if (Prev != null) {
            length = Prev.length - 1;
        }
        if (length == 0) return;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position += transform.position + Vector3.forward;
        FollowChain fc = go.AddComponent<FollowChain>();
        
        Next = fc;
        fc.Prev = this;
        fc.Spawn(this);

    }
}

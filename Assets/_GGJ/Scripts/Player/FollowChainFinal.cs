using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChainFinal : MonoBehaviour {
    public int length=-1;
    public float distance {
        get { return offset.magnitude; }
    }

    public FollowChainFinal Next;
    public FollowChainFinal Prev;
    public GameObject joint;
    public Vector3 offset;
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
        transform.position = Vector3.Lerp(transform.position, Prev.transform.position + Prev.transform.TransformVector(offset), Time.deltaTime * 10);
        transform.rotation = Quaternion.LookRotation(dir,transform.up);
        transform.Rotate(0, -90, 0);
        float rDotu = Vector3.Dot(transform.forward, Prev.transform.up);
        transform.Rotate(0, 0, rDotu*Time.deltaTime*200);
    }
    public void Spawn(FollowChainFinal from) {
        if(Prev!= null) {
            joint = new GameObject();
            joint.transform.parent = transform.parent;
            joint.transform.localPosition = transform.localPosition;
            joint.transform.rotation = transform.rotation;
            offset = transform.localPosition;
            transform.parent = null;
        }
        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (transform.childCount == 0) return;
        Transform trans = transform.GetChild(0);
        if (trans == null) return;
        FollowChainFinal fc = trans.gameObject.AddComponent<FollowChainFinal>();
        
        Next = fc;
        fc.Prev = this;
        fc.Spawn(this);

    }
}

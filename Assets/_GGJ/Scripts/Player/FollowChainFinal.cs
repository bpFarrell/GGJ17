using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChainFinal : MonoBehaviour {
    public int length=-1;
    public float distance {
        get { return offset.magnitude; }
    }
    public static GameObject rootNode;
    public FollowChainFinal Next;
    public FollowChainFinal Prev;
    public GameObject joint;
    public GameObject dummy;
    public float transStiffness = 1;
    public float rotStiffness = 1;
    public Vector3 offset;
    public int frameCount = 0;
    public int counter = 0;
    void Awake() {
        if (rootNode == null) {
            rootNode = new GameObject("RootNode");
			rootNode.transform.parent = FindObjectOfType<DelayEnable>().transform;
        }
        if (length == -1) return;
        Spawn(null);
    }
    void Update() {
        if (frameCount < 10+counter) {
            frameCount++;
            return;
        }
        if (Prev == null) return;
        Vector3 dir = transform.position - Prev.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        if (dist < distance) {
            transform.position = Prev.transform.position + dir * distance;
        }
        dummy.transform.position = Vector3.Lerp(dummy.transform.position, Prev.transform.position + Prev.transform.TransformVector(offset), Time.deltaTime * 20 * transStiffness);
        dummy.transform.rotation = Quaternion.LookRotation(dir,dummy.transform.up);
        float rDotu = Vector3.Dot(dummy.transform.right, Prev.transform.up);
        dummy.transform.Rotate(0, 0, -rDotu*Time.deltaTime*200*rotStiffness);
    }
    public void Spawn(FollowChainFinal from) {
        if(Prev!= null) {
            counter = Prev.counter + 5;
            dummy = new GameObject(transform.name);
            dummy.transform.parent = rootNode.transform;
            dummy.transform.position = transform.position;
            dummy.transform.rotation= Quaternion.LookRotation(transform.position-Prev.transform.position, Vector3.up);
            offset = transform.localPosition;
            transStiffness = Prev.transStiffness;
            rotStiffness = Prev.rotStiffness;
            transform.parent = dummy.transform;
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

﻿using System.Collections;
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
    public float stiffness = 1;
    public Vector3 offset;
    void Awake() {
        if (rootNode == null) {
            rootNode = new GameObject("RootNode");
        }
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
        dummy.transform.position = Vector3.Lerp(dummy.transform.position, Prev.transform.position + Prev.transform.TransformVector(offset), Time.deltaTime * 10 * stiffness);
        dummy.transform.rotation = Quaternion.LookRotation(dir,dummy.transform.up);
        float rDotu = Vector3.Dot(dummy.transform.right, Prev.transform.up);
        dummy.transform.Rotate(0, 0, -rDotu*Time.deltaTime*200*stiffness);
    }
    public void Spawn(FollowChainFinal from) {
        if(Prev!= null) {
            dummy = new GameObject(transform.name);
            dummy.transform.parent = rootNode.transform;
            dummy.transform.position = transform.position;
            dummy.transform.rotation= Quaternion.LookRotation(transform.position-Prev.transform.position, Vector3.up);
            offset = transform.localPosition;
            stiffness = Prev.stiffness;
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

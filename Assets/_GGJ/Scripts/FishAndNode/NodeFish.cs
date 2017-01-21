using UnityEngine;
using System.Collections;

public class NodeFish : MonoBehaviour {

    Transform parentNode;
    Vector3 dir;
    float dist;
    Vector3 desiredPos {
        get {
            return (parentNode.position + (dir.normalized * dist));
        }
    }
    public void Init(Transform parent, Vector3 assignedPos) {
        parentNode = parent;
        dir = (assignedPos - parent.position);
        dist = dir.magnitude;
        
    }
	// Use this for initialization
	void Start () {

 
	}
	
	// Update is called once per frame
	void Update () {
     //   Debug.Log(dir + " : " + dist);
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class NodeFish : MonoBehaviour {

    public enum State {
        neutral,
        travel,
        mother
    }
    public State state;

    Transform parentNode;
    public FishControl fishControl;

    Vector3 dir;
    public float dist;
    public float defaultDist;
    Vector3 desiredPos {
        get {
            return (parentNode.position + (dir.normalized * dist));
        }
    }
    public void Init(Transform parent, Vector3 assignedPos, Transform fishParent) {
        parentNode = parent;
        dir = (assignedPos - parent.position);
        dist = dir.magnitude;
        defaultDist = dist;

        GameObject fish = Instantiate(Resources.Load("fish")) as GameObject;
        fish.transform.position = transform.position;
        fish.transform.localScale *= Random.Range(0.8001f, 1.2001f);
    //    fish.GetComponent<FishBehavior>().target = transform;
        fishControl = fish.GetComponent<FishControl>();
        fishControl.target = transform;
        fish.transform.SetParent(fishParent);
    }
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
     //   Debug.Log(dir + " : " + dist);
        transform.position = Vector3.Lerp(transform.position, desiredPos, 20*Time.deltaTime);
        if (state == State.travel) {
            dist = defaultDist * 0.5f;
            fishControl.fwdSpeed = Random.Range(5,9);
        }
        if (state == State.neutral) {
            dist = defaultDist * 1.5f;
            fishControl.fwdSpeed = fishControl.rotSpeed = Random.Range(0.75f,1.5f);
        }
        if (state == State.mother) {
            dist = defaultDist * 0.5f;
            fishControl.fwdSpeed = Random.Range(5, 15);
        }
    }
}

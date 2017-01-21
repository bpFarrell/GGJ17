using UnityEngine;
using System.Collections;

public class FishBehavior : MonoBehaviour {

	public float fwdSpeed = 1;
    public float rotSpeed = 5;
    Vector3 target;
	enum State{
		none,
		idle,
        avoidance,
		herd,
		scared,
		mother
	}

	State state;
    State prevState;

    Quaternion newRot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * fwdSpeed * Time.deltaTime;
        StateMachine(state);
    }

	void StateMachine(State state){
		switch (state) {
		case State.idle:
			// look for node;
			break;
        case State.avoidance:
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed * Time.deltaTime);

                break;
		case State.herd:
			// try to regroup;
			break;
		case State.scared:
			// try to regroup;
			break;
		case State.mother:
			// try to regroup;
			break;

		}
	}


    void OnTriggerEnter(Collider col){
    //    Debug.Log("bam");
		if (col.GetComponent<FishBehavior> () != null &&
            Vector3.Dot((col.transform.position - transform.position),transform.forward) > 0) {
            newRot = col.transform.rotation;
            prevState = state;
            state = State.avoidance;
            //    Debug.Log("fish");
        }
	}
	void Lingering(){
		
	}
	void FindNearestNode(){
	
	}
}

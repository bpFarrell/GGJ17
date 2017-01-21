using UnityEngine;
using System.Collections;

public class FishBehavior : MonoBehaviour {

    public Transform target;
	public enum State{
		none,
		idle,
		herd,
		mother
	}

	public State state;
    State prevState;

    public enum SubState {
        normal,
        avoidance,
        scared
    }
    public SubState subState;

    Quaternion newRot;
    Vector3 adjTargetPos;
    Vector3 adjDir;
	// Use this for initialization
	void Start () {
        transform.Rotate(Vector3.up, Random.Range(0.0f, 360.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
        StateMachine(state);
        SubStateMachine(subState);
        if (target != null)
        {
            adjTargetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            adjDir = (adjTargetPos - transform.position);
            if (subState != SubState.avoidance &&
                (transform.position - adjTargetPos).magnitude > 5 &&
                Vector3.Dot(transform.forward, adjDir) < 0.9f)
            {
                subState = SubState.scared;
            }
         //   else if (subState != SubState.avoidance) {
         //       subState = SubState.normal;
         //   }
        }
        
    }
    void SubStateMachine(SubState subState) {
        switch (subState) {
            case SubState.normal:
                break;
            case SubState.scared:
                
                
           //     newRot = Quaternion.FromToRotation(transform.forward, adjDir);
           //     transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed * Time.deltaTime);

                // try to regroup;
                break;
            case SubState.avoidance:
           //     transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed * Time.deltaTime);

                break;
        }
    }
	void StateMachine(State state){
		switch (state) {
		case State.idle:
			// look for node;
			break;
        
		case State.herd:
  //              transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed * Time.deltaTime);

                // try to regroup;
                break;
	//	case State.scared:
    //            Vector3 adjTargetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
    //            Vector3 adjDir = (adjTargetPos - transform.position);
    //            newRot = Quaternion.FromToRotation(transform.forward, adjDir);
    //            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotSpeed * Time.deltaTime);
    //
    //            break;
		case State.mother:
			// try to regroup;
			break;

		}
	}


//   void OnTriggerEnter(Collider col){
//   //    Debug.Log("bam");
//		if (col.GetComponent<FishBehavior> () != null &&
//           Vector3.Dot((col.transform.position - transform.position),transform.forward) > 0) {
//            newRot = col.transform.rotation;
//            subState = SubState.avoidance;
//       //    prevState = state;
//       //    state = State.herd;
//           //    Debug.Log("fish");
//       }
//	}
	void Lingering(){
		
	}
	void FindNearestNode(){
	
	}
}

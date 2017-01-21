using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleLogic : MonoBehaviour {
    PlayerController pc;
    float targetRot {
        get { return Mathf.Lerp(rotMin, rotMax, t); }
    }
    public float t = 0;
    float rotMax = 100;
    float rotMin=60;
    public enum State {
        Contract,
        Expand
    }
    public State state = State.Expand;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
        if (state == State.Contract) {
            t -= Time.deltaTime*5;
            if (t < 0) {
                state = State.Expand;
            }
        } else if(state==State.Expand) {
            t += Time.deltaTime;
        }
        t = Mathf.Clamp01(t);
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.z = targetRot;
        Quaternion target = Quaternion.Euler(rot);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime);
    }
    public void Init(PlayerController pc) {
        this.pc = pc;
        pc.flutter += Flutter;
    }
    public void Flutter() {
        state = State.Contract;
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = targetRot;
        transform.rotation = Quaternion.Euler(rot);
    }

}

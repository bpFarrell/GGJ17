using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleLogic : MonoBehaviour {
    PlayerController pc;
    Quaternion targetRot {
        get { return Quaternion.LerpUnclamped(rotMin, rotMax, t+(Mathf.Cos(Time.time*1.3f))*0.5f); }
    }
    public float t = 0;
    Quaternion rotMax;
    Quaternion rotMin;
    public enum State {
        Contract,
        Expand
    }
    public State state = State.Expand;
	// Use this for initialization
	// Update is called once per frame
    void Awake() {
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.z = 60;
        rotMax = Quaternion.Euler(rot);
        rot.z = 100;
        rotMin = Quaternion.Euler(rot);
    }
	void Update () {
        if (state == State.Contract) {
            t -= Time.deltaTime*2;
            if (t < 0) {
                state = State.Expand;
            }
        } else if(state==State.Expand) {
            t += Time.deltaTime;
        }
        t = Mathf.Clamp01(t);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime*100);
    }
    public void Init(PlayerController pc) {
        this.pc = pc;
        pc.flutter += Flutter;
    }
    public void Flutter() {
        state = State.Contract;
    }

}

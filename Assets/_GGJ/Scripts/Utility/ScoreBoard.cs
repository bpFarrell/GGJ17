using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
    [Range(0,1)]
    public float t;
    [Range(0, 1)]
    public float weight=0.5f;
    float direction {
        get {
            if (weight > 0.5) {
                return 1;
            }
            return -1;
        }
    }
    bool normalized= true;
    float playSpeed = 0.3f;
    float pushSpeed = 1;
    float idleTime = 1;
    float completedTime;
    public bool play = false;
    public Material mat;
	// Use this for initialization
    public enum State {
        Counter,
        Idle,
        Push
    }
    public State state = State.Counter;
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        weight = GameInfo.main.fishRation;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.Counter:
                Counter();
                break;
            case State.Idle:
                Idle();
                break;
            case State.Push:
                Push();
                break;
        }
        Draw();
	}
    void Counter() {
        if (play) {
            t = Mathf.Clamp01(t + Time.deltaTime * playSpeed);
            if (t == 1) {
                //t = 0;
                play = false;
                state = State.Idle;
                completedTime = Time.time;
            }
        }
    }
    void Idle() {
        if (Time.time > completedTime + idleTime) {
            state = State.Push;
        }
    }
    void Push() {
        weight = Mathf.Clamp01(weight += pushSpeed * direction * Time.deltaTime);
    }
    void Draw() {
        float jitter = 0;
        if (state == State.Idle) {
            jitter = Mathf.Sin(Time.time * 50) * 0.005f;
        }
        if (normalized) {
            mat.SetVector("_T", new Vector4((t * (weight+jitter)), ((t * 0.9f + 0.1f) * (1 - (weight+jitter))), 0, 0));
        } else {
            //old
            mat.SetVector("_T", new Vector4(Mathf.Min(t, weight), Mathf.Min(t, 1 - weight), 0, 0));
        }
    }
}

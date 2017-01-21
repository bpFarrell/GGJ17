using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector3 velocity = new Vector3();
    public float friction = 1;
    public float power = 5;
    float lastPressTime;
    GameObject roll;
    float twistDelay = 0.2f;
    public IList<GameObject> tents = new List<GameObject>();
    public delegate void func();
    float twist = 360;
    public event func flutter;
	// Use this for initialization
    void Awake() {
        roll = transform.GetChild(0).gameObject;
    }
	void Start () {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children) {
            if (child.name.Contains("octoKnee")) {
                TentacleLogic tl = child.gameObject.AddComponent<TentacleLogic>();
                tl.Init(this);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("P1_A")) {
            velocity += transform.forward*power;
            flutter();
            if (lastPressTime + twistDelay > Time.time) {
                if(twist>0)
                    twist -=360;
            }
            lastPressTime = Time.time;
        }
        float deltaTwist = CalculateTwist();
        float levelOut = LevelOut();
        float angle = Input.GetAxis("P1_LeftHorizontal");
        transform.Rotate(0, angle * Time.deltaTime*100, 0);
        roll.transform.Rotate(0, 0, deltaTwist);
        

        transform.position += velocity * Time.deltaTime;
        velocity = velocity - (velocity * Time.deltaTime*friction);
	}
    float CalculateTwist() {

        float old = twist;
        twist += Time.deltaTime * 500;
        twist = Mathf.Min(twist, 360);
        return  old - twist;
    }
    float LevelOut() {
        return Vector3.Dot(transform.forward, Vector3.up);
    }
}

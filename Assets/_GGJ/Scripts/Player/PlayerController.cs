using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isPlayerOne;
	public string playerPrefix = "P1_";
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
        Mesh mesh;
        roll = transform.GetChild(0).gameObject;
        Animation[] anim = GetComponentsInChildren<Animation>();
        for(int x = 0; x < anim.Length; x++) {
            anim[x].Play();
        }
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
        if (Input.GetButtonDown(playerPrefix + "A")||Input.GetKeyDown(KeyCode.Space)) {
            velocity += transform.forward*power;
            flutter();
            if (lastPressTime + twistDelay > Time.time) {
                if(twist>0)
                    twist -=360;
            }
            lastPressTime = Time.time;
        }
        CalculateIdle();
        BoundCheck();
        float deltaTwist = CalculateTwist();
        float levelOut = LevelOut();
        float angle = Input.GetAxis(playerPrefix + "LeftHorizontal");
        if (Input.GetKey(KeyCode.A)) angle = -1;
        if (Input.GetKey(KeyCode.D)) angle = 1;
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
    void CalculateIdle() {
        float scale = 1;
        float speed = 1;
        Vector3 offset = new Vector3(
            Mathf.Sin(Time.time * speed) * scale,
            Mathf.Cos(Time.time * speed*1.3f) * scale,
            - Mathf.Cos(Time.time * speed) * scale
            );
        roll.transform.localPosition = offset;
    }
    void BoundCheck() {
        float force=50;
        if (transform.position.x < 50) {
            velocity.x += Time.deltaTime * force;
        }
        if (transform.position.z < 50) {
            velocity.z += Time.deltaTime * force;
        }
        if (transform.position.x > 450) {
            velocity.x -= Time.deltaTime * force;
        }
        if (transform.position.z > 450) {
            velocity.z -= Time.deltaTime * force;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum InputType {
        Tank,
        CamerarRlative
    }
    public InputType inputType = InputType.CamerarRlative;
    float psTime;
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
    public static bool locked;
    public Camera cam;
    void Awake() {
        Mesh mesh;
        locked = false;
        roll = transform.GetChild(0).gameObject;
        Animation[] anim = GetComponentsInChildren<Animation>();
        for(int x = 0; x < anim.Length; x++) {
            anim[x].Play();
        }
        cam = transform.parent.GetComponentInChildren<Camera>();
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
        float angle = 0;
        if (!locked) {
            if (Input.GetButtonDown(playerPrefix + "A") || Input.GetKeyDown(KeyCode.Space)) {
                velocity += transform.forward * power;
                flutter();
                if (lastPressTime + twistDelay > Time.time) {
                    if (twist > 0)
                        twist -= 360;
                }
                lastPressTime = Time.time;
            }
            angle = Input.GetAxis(playerPrefix + "LeftHorizontal");
            if (Input.GetKey(KeyCode.A)) angle = -1;
            if (Input.GetKey(KeyCode.D)) angle = 1;
        }
        CalculateIdle();
        BoundCheck();
        float deltaTwist = CalculateTwist();
        float levelOut = LevelOut();
        if (inputType == InputType.CamerarRlative) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateInput(), Time.deltaTime * 125);
        } else if (inputType == InputType.CamerarRlative) {
            transform.Rotate(0, angle * Time.deltaTime*100, 0);
        }
        roll.transform.Rotate(0, 0, deltaTwist);
        

        transform.position += velocity * Time.deltaTime;
        velocity = velocity - (velocity * Time.deltaTime*friction);
	}
    Quaternion CalculateInput() {
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();


        Vector3 target = Vector3.zero;

        float angleX = Input.GetAxis(playerPrefix + "LeftHorizontal");

        if (Input.GetKey(KeyCode.A)) angleX = -1;
        if (Input.GetKey(KeyCode.D)) angleX = 1;
        target += camRight * angleX;


        float angleY = -Input.GetAxis(playerPrefix + "LeftVertical");

        if (Input.GetKey(KeyCode.W)) angleY = 1;
        if (Input.GetKey(KeyCode.S)) angleY = -1;
        target += camForward * angleY;


        if (target.magnitude<0.1) {
            target = transform.forward;
        }
        return Quaternion.LookRotation(target,transform.up);
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
        float force=100;
        if (transform.position.x < 250) {
            ForceParticle(90);
            velocity.x += Time.deltaTime * force;
        }
        if (transform.position.z < 250) {
            ForceParticle(0);
            velocity.z += Time.deltaTime * force;
        }
        if (transform.position.x > 750) {
            ForceParticle(90);
            velocity.x -= Time.deltaTime * force;
        }
        if (transform.position.z > 750) {
            ForceParticle(0);
            velocity.z -= Time.deltaTime * force;
        }
    }
    void ForceParticle(float yRotate) {
        if (Time.time > psTime + 1.0f)
        {
            psTime = Time.time;
            GameObject ps = Instantiate(Resources.Load("psStop")) as GameObject;
            ps.transform.rotation = Quaternion.AngleAxis(yRotate, Vector3.up);// (Vector3.up, yRotate);
       //     ParticleSystem.MainModule mainMod = ps.GetComponent<ParticleSystem>().main;
       //     mainMod.startRotation3D = true;
       //     mainMod.startRotationY = yRotate;
            ps.transform.position = transform.position;

        }
    }
}

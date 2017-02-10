using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLogic : MonoBehaviour {

    public Vector3 velocity = new Vector3();
    public float friction = 1;
    public float power = 20;
    float lastPressTime;
    GameObject roll;
    float twistDelay = 0.2f;
    float twist = 360;
    float psTime;
    bool queued = false;
    float timeToPush;
    int frameCount;
    // Use this for initialization
    void Awake () {

        roll = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (frameCount < 40) {
            frameCount++;
            return;
        }
        CalculateIdle();
        BoundCheck();
        if (Time.time > timeToPush&&queued)
            ApplyForce();
        float deltaTwist = CalculateTwist();
        float levelOut = LevelOut();
        roll.transform.Rotate(0, 0, deltaTwist);


        transform.position += velocity * Time.deltaTime;
        velocity = velocity - (velocity * Time.deltaTime * friction);
    }
    public void Go() {
        PrepGo();
        if (lastPressTime + twistDelay > Time.time) {
            if (twist > 0)
                twist -= 360;
        }
        lastPressTime = Time.time;
    }
    void PrepGo() {
        if (queued) return;
        queued = true;
        timeToPush = Time.time + 0.2f;
    }
    void ApplyForce() {
        queued = false;
        velocity += transform.forward * power;
    }
    float CalculateTwist() {

        float old = twist;
        twist += Time.deltaTime * 500;
        twist = Mathf.Min(twist, 360);
        return old - twist;
    }
    float LevelOut() {
        return Vector3.Dot(transform.forward, Vector3.up);
    }
    void CalculateIdle() {
        float scale = 1;
        float speed = 1;
        Vector3 offset = new Vector3(
            Mathf.Sin(Time.time * speed) * scale,
            Mathf.Cos(Time.time * speed * 1.3f) * scale,
            -Mathf.Cos(Time.time * speed) * scale
            );
        roll.transform.localPosition = offset;
    }
    void BoundCheck() {
        float force = 100;
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
        if (Time.time > psTime + 1.0f) {
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

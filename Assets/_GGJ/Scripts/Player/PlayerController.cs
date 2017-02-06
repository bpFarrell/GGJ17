using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum InputType {
        Tank,
        CamerarRlative
    }
    public InputType inputType = InputType.CamerarRlative;
    public bool isPlayerOne;
	public string playerPrefix = "P1_";
    public IList<GameObject> tents = new List<GameObject>();
    public delegate void func();
    public event func flutter;
    public static bool locked;
    public TorsoLogic torso;
    public Camera cam;
    MoveLogic ml;
    void Awake() {
        Mesh mesh;
        locked = false;
        Animation[] anim = GetComponentsInChildren<Animation>();
        for(int x = 0; x < anim.Length; x++) {
            anim[x].Play();
        }
        cam = transform.parent.GetComponentInChildren<Camera>();
        torso = transform.GetComponentInChildren<TorsoLogic>();
        if (torso == null) {
            Debug.LogError("Torse not set! Attack torso logic to octoTorso");
        }
        ml = gameObject.AddComponent<MoveLogic>();
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
                ml.Go();
                flutter();
                torso.Woosh();
            }
            angle = Input.GetAxis(playerPrefix + "LeftHorizontal");
            if (Input.GetKey(KeyCode.A)) angle = -1;
            if (Input.GetKey(KeyCode.D)) angle = 1;
        }
        if (inputType == InputType.CamerarRlative) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateInput(), Time.deltaTime * 125);
        } else if (inputType == InputType.CamerarRlative) {
            transform.Rotate(0, angle * Time.deltaTime*100, 0);
        }
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
}

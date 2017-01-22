using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMain : MonoBehaviour {

	public enum State{
		neutral,
		travel,
		mother
	}

    public State state;

    public IList<NodeFish> fishNodes = new List<NodeFish>();
    public float offsetDistToCore = 1;
    public bool stateChange;

    public bool isPlayerOne;
    public Transform trackTarget;

    public float travelSpeed = 10;

    public ParticleSystem particle;
    public ParticleSystem.MainModule psMain;
    float dropTime;
    float coolDown = 3;

    Transform intruder;
    float intrudeTimeStart;
    bool isIntrude;
    float radius;
	// Use this for initialization
	void Start () {
        //  Init(pos);
        particle = GetComponentInChildren<ParticleSystem>();
        psMain = particle.main;

        radius = GetComponent<SphereCollider>().radius;
        Debug.Log(radius);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.forward * Mathf.Sin(Time.time) * 0.01f;
        transform.position += Vector3.right * Mathf.Sin(Time.time) * 0.01f;
        if (stateChange) {
            stateChange = false;
            for (int i = 0; i < fishNodes.Count; i++) {
                fishNodes[i].dist = fishNodes[i].defaultDist * offsetDistToCore;
            }
        }

        if (state == State.travel) {
            transform.LookAt(trackTarget);
            transform.position += transform.forward * travelSpeed * Time.deltaTime;
            if ((trackTarget.position - transform.position).magnitude > 20) {
                SetToNeutralState();
            }
        }

        if (state == State.mother) {
            transform.LookAt(trackTarget);
            if ((transform.position - trackTarget.position).magnitude > 10)
            {
                transform.position += transform.forward * travelSpeed * Time.deltaTime;
            }
            if (isPlayerOne)
            {
                psMain.startColor = new Color(0f, 0.5f, 1.0f, 0.15f);
            }
            else {
                psMain.startColor = new Color(1f, 0.0f, 0.0f, 0.15f);
            }
        }

        if (isIntrude) {
            IntruderStay();
        }
    }

    public void SetToNeutralState() {
        state = State.neutral;
        for (int i = 0; i < fishNodes.Count; i++)
        {
            //     fishNodes[i].dist = fishNodes[i].defaultDist * 0.5f;
            fishNodes[i].state = NodeFish.State.neutral;
            fishNodes[i].fishControl.SetColor(0);
        }
        Debug.Log(state);
    }
    public void Init(Vector3 origin, int amountFish, Transform rootParent, Transform fishParent) {
        float degree = 360 / amountFish;
        for (int i = 0; i < amountFish; i++) {
            Vector3 temp = Tools.PointOnCircle(origin, degree * i, Random.Range(1,amountFish*0.1f));
            Vector3 assignedPos = new Vector3(temp.x, Random.Range(origin.y, origin.y+amountFish*0.1f), temp.z);
            GameObject fishNodeGO = new GameObject("fishNode");//GameObject.CreatePrimitive(PrimitiveType.Cube);
            //new GameObject("fishNode");//GameObject.CreatePrimitive(PrimitiveType.Cube);
            //        fishNodeGO.GetComponent<MeshRenderer>().enabled = false;
            fishNodeGO.transform.position = assignedPos;//new Vector3(temp.x, Random.Range(0, 5), temp.z);
            fishNodeGO.transform.localScale *= 0.5f;

            fishNodeGO.transform.SetParent(rootParent);
            NodeFish fish = fishNodeGO.AddComponent<NodeFish>();
            fish.Init(transform, assignedPos, fishParent);

            fishNodes.Add(fish);
        }
    }
    void OnTriggerEnter(Collider col) {
        Debug.Log(col.tag);
        if (col.tag != "Player" ||
            state == State.mother) return;
        Debug.Log(col.name);
        trackTarget = col.transform;
        state = State.travel;
        if (state == State.travel) {
            if (isPlayerOne != col.GetComponent<PlayerController>().isPlayerOne) {
                intruder = col.transform;
                intrudeTimeStart = Time.time;
                isIntrude = true;
            }
        }
        if (col.GetComponent<PlayerController>().isPlayerOne)
        {
            isPlayerOne = true;
            for (int i = 0; i < fishNodes.Count; i++)
            {
                //     fishNodes[i].dist = fishNodes[i].defaultDist * 0.5f;
                fishNodes[i].state = NodeFish.State.travel;
                fishNodes[i].fishControl.SetColor(1);
            }
        }
        else
        {
            isPlayerOne = false;
            for (int i = 0; i < fishNodes.Count; i++)
            {
                //     fishNodes[i].dist = fishNodes[i].defaultDist * 0.5f;
                fishNodes[i].state = NodeFish.State.travel;
                fishNodes[i].fishControl.SetColor(2);
            }
        }
    }

    void IntruderStay() {
        float dist = (intruder.position - transform.position).magnitude;

        if (dist > radius) {
            isIntrude = false;
            Debug.Log("failed to steal");
            return;
        }
        if (Time.time > intrudeTimeStart + 2.0f) {
            // drop the node;
            SetToNeutralState();
        }
    }
 //   void OnTriggerLeave(Collider col) {
 //       if (col.tag == "Player")
 //       {
 //           Debug.Log("Leavingggggggggggggggg");
 //           state = State.neutral;
 //       }
 //   }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMain : MonoBehaviour {

	enum State{
		neutral,
		travel,
		mother
	}

    State state;

    IList<NodeFish> fishNodes = new List<NodeFish>();
    public float offsetDistToCore = 1;
    public bool stateChange;

    bool isPlayerOne;
    Transform player;

    public float travelSpeed = 10;
	// Use this for initialization
	void Start () {
      //  Init(pos);
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
            transform.LookAt(player);
            transform.position += transform.forward * travelSpeed * Time.deltaTime;
        }
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
        if (col.tag != "Player") return;
        Debug.Log(col.name);
        player = col.transform;
        state = State.travel;
        if (col.GetComponent<PlayerController>().isPlayerOne)
        {
            isPlayerOne = true;
        }
        else isPlayerOne = false;
    }
    void OnTriggerLeave(Collider col) {
        if (col.tag == "Player")
        {
            Debug.Log("Leavingggggggggggggggg");
            state = State.neutral;
        }
    }
}

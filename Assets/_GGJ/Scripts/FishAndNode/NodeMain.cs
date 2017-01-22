using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMain : MonoBehaviour {

	enum State{
		neutral,
		travel,
		mother
	}

    IList<NodeFish> fishNodes = new List<NodeFish>();
    public float offsetDistToCore = 1;
    public bool stateChange;
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
    }

    public void Init(Vector3 origin, int amountFish, Transform rootParent, Transform fishParent) {
        float degree = 360 / amountFish;
        for (int i = 0; i < amountFish; i++) {
            Vector3 temp = Tools.PointOnCircle(origin, degree * i, Random.Range(2,5));
            Vector3 assignedPos = new Vector3(temp.x, Random.Range(origin.y, origin.y+5), temp.z);
            GameObject fishNodeGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            fishNodeGO.GetComponent<MeshRenderer>().enabled = false;
            fishNodeGO.transform.position = assignedPos;//new Vector3(temp.x, Random.Range(0, 5), temp.z);
            fishNodeGO.transform.localScale *= 0.5f;

            fishNodeGO.transform.SetParent(rootParent);
            NodeFish fish = fishNodeGO.AddComponent<NodeFish>();
            fish.Init(transform, assignedPos, fishParent);

            fishNodes.Add(fish);
        }
    }
}

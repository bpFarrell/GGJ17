using UnityEngine;
using System.Collections;

public class NodeMain : MonoBehaviour {

	enum State{
		neutral,
		travel,
		mother
	}
	// Use this for initialization
	void Start () {
      //  Init(pos);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(Vector3 origin, int amountFish) {
        float degree = 360 / amountFish;
        for (int i = 0; i < amountFish; i++) {
            Vector3 temp = Tools.PointOnCircle(origin, degree * i, Random.Range(2,5));
            Vector3 assignedPos = new Vector3(temp.x, Random.Range(0, 5), temp.z);
            GameObject fishNodeGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            fishNodeGO.transform.position = assignedPos;//new Vector3(temp.x, Random.Range(0, 5), temp.z);
            fishNodeGO.transform.localScale *= 0.5f;

            NodeFish fish = fishNodeGO.AddComponent<NodeFish>();
            fish.Init(transform, assignedPos);

        }
    }
}

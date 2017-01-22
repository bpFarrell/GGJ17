using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

	int amountMainNode;
	int amountFishNOde;
	float distToInnerBound;
	float distToOuterBound;

	public Vector3 motherPos;
    public GameObject rootParent;
    public GameObject fishParent;

	public IList<NodeMain> mainNodes = new List<NodeMain>();
	// Use this for initialization
	void Start () {
        rootParent = new GameObject("rootParent");
        fishParent = new GameObject("fishParent");
        fishParent.AddComponent<FishManager>();

        motherPos = transform.position;
        GameInfo gameInfo = GetComponent<GameInfo>();
        amountMainNode = gameInfo.amountMainNodes;
        amountFishNOde = Mathf.RoundToInt(gameInfo.amountFishToSpawn / amountMainNode);
        distToInnerBound = gameInfo.distToInnerBound;
        distToOuterBound = gameInfo.distToOuterBound;

		NodePlacement ();
	}
	
	// Update is called once per frame
	void Update () {
        motherPos = transform.position;
	}

	void NodePlacement(){
		float sliceDegree = 360.0f / amountMainNode;
		for (int i = 0; i < amountMainNode; i++) {
         //   Debug.Log("mother "+motherPos);
			Vector3 pos = Tools.PointOnCircle (motherPos, sliceDegree * i, Random.Range (distToInnerBound, distToOuterBound));
        //    Debug.Log("circ p "+pos);
            GameObject nodeGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nodeGO.transform.position = pos;
            NodeMain nodeMain = nodeGO.AddComponent<NodeMain>();
            nodeMain.Init(pos,amountFishNOde, rootParent.transform, fishParent.transform);
            //    NodeMain nodeMain = new NodeMain (pos);
			mainNodes.Add (nodeMain);
            nodeGO.transform.SetParent(rootParent.transform);
		}
	}

}

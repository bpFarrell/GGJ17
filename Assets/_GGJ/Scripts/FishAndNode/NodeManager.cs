using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

	int amountMainNode;
	int amountFishNOde;
	float distToInnerBound;
	float distToOuterBound;

	public Vector3 motherPos;

	public IList<NodeMain> mainNodes = new List<NodeMain>();
	// Use this for initialization
	void Start () {
        GameInfo gameInfo = GetComponent<GameInfo>();
        amountMainNode = gameInfo.amountMainNodes;
        amountFishNOde = Mathf.RoundToInt(gameInfo.amountFishToSpawn / amountMainNode);
        distToInnerBound = gameInfo.distToInnerBound;
        distToOuterBound = gameInfo.distToOuterBound;

		NodePlacement ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void NodePlacement(){
		float sliceDegree = 360.0f / amountMainNode;
		for (int i = 0; i < amountMainNode; i++) {
			Vector3 pos = Tools.PointOnCircle (motherPos, sliceDegree * i, Random.Range (distToInnerBound, distToOuterBound));
            GameObject nodeGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nodeGO.transform.position = pos;
            NodeMain nodeMain = nodeGO.AddComponent<NodeMain>();
            nodeMain.Init(pos,amountFishNOde);
            //    NodeMain nodeMain = new NodeMain (pos);
			mainNodes.Add (nodeMain);
		}
	}

}

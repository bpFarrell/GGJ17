using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {

	public int amountMainNode;
	public int amountFishNOde;
	public float distToInnerBound;
	public float distToOuterBound;

	public Vector3 motherPos;

	public IList<NodeMain> mainNodes = new List<NodeMain>();
	// Use this for initialization
	void Start () {
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
            nodeMain.Init(pos,10);
            //    NodeMain nodeMain = new NodeMain (pos);
			mainNodes.Add (nodeMain);
		}
	}

}

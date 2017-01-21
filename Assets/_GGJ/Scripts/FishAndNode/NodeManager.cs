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
			Vector3 pos = PointOnCircle (motherPos, sliceDegree * i, Random.Range (distToInnerBound, distToOuterBound));
			GameObject node = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			node.transform.position = pos;
			NodeMain nodeMain = new NodeMain ();
			mainNodes.Add (nodeMain);
		}
	}

	Vector3 PointOnCircle(Vector3 origin, float angleInDegrees, float radius){
		float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
		float z = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.z;
		return new Vector3 (x,origin.y,z);
	}
}

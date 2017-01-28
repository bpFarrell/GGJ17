using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointFollowChain : MonoBehaviour 
{

	public class BoneNode
	{
		public Transform parent;
		public Transform child;
		public Transform self;
		public Vector3 lastPosition;
		public Vector3 targetPosition;
		public float distanceToChild;
		public float distanceToParent;

		public void update()
		{
			if(parent == null) return;
			self.position = self.position + ((targetPosition - self.position) * .07f);
			self.LookAt(parent.position);
		}

		public void reverseAssociation()
		{
			var i = parent;
			parent = child;
			child = i;
		}
	}
	public List<BoneNode> bonePile = new List<BoneNode>();

	public Transform[] childContainer;

	// Use this for initialization
	void Start () 
	{
		childContainer = GetComponentsInChildren<Transform>();
		BoneNode bone = new BoneNode();
		//bone.parent = null;
		//bone.child = childContainer[0];
		//bone.self = transform;
		//bone.lastPosition = transform.position;
		//bone.distanceToParent = 0.0f;
		//bone.distanceToChild = (bone.self.transform.position - bone.child.transform.position).magnitude;
		//bonePile.Add(bone);
		
		for (int i = 0; i < childContainer.Length; i++) 
		{
			bone = new BoneNode();
			if(i <= 0) 
				bone.parent = null;
			else
				bone.parent = childContainer[i-1];

			if(i >= childContainer.Length-1)
				bone.child = null;
			else 
				bone.child = childContainer[i+1];

			bone.self = childContainer[i];
			bone.lastPosition = bone.self.position;

			if(bone.child != null) bone.distanceToChild = (bone.self.transform.position - bone.child.transform.position).magnitude;
			if(bone.parent != null) bone.distanceToParent = (bone.self.transform.position - bone.parent.transform.position).magnitude;

			bonePile.Add(bone);
		}
		foreach (Transform child in childContainer) {
			child.parent = null;
		}
		//bonePile.Reverse();
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (BoneNode bone in bonePile) 
		{
			if(bone.parent == null) continue;
			if((bone.parent.position - bone.self.position).magnitude < bone.distanceToParent) 
			{
				bone.targetPosition = bone.parent.position + ((bone.self.position - bone.parent.position).normalized * bone.distanceToParent);
			}
			else if ((bone.parent.position - bone.self.position).magnitude > bone.distanceToParent)
			{
				bone.targetPosition = bone.parent.position + ((bone.parent.position - bone.self.position).normalized * bone.distanceToParent);
			}
		}
		foreach(BoneNode bone in bonePile)
		{
			bone.update();
		}
	}
}

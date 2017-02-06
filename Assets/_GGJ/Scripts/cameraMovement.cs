using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour 
{
	public float followSpeed;
	public Transform cameraFollow;
	public Transform lookTarget;
	public Vector3 targetOffset = Vector3.zero;
    NodeManager nodeManage;
    float distFromMother;

	private Vector3 lookAtPos = new Vector3 ();
	// Use this for initialization
	void Start () 
	{
		transform.position = cameraFollow.position;
		transform.LookAt(lookTarget.position + targetOffset);

        nodeManage = FindObjectOfType<NodeManager>();
	}

	Vector3 thisPos = Vector3.zero;

	// Update is called once per frame
	void Update () 
	{
		
        distFromMother = (nodeManage.transform.position - lookTarget.position).magnitude;
        if (distFromMother < 100)
        {
            LookAtMother();
        }
        else
        {
            Vector3 displacement = cameraFollow.position - transform.position;
            float step = displacement.magnitude * followSpeed / 100;

            Vector3 tempPos = transform.position;
            tempPos += (displacement.normalized * step);

            transform.position = tempPos;
			Vector3 playerPos = lookTarget.position + targetOffset;
			playerPos.y = lookTarget.position.y + targetOffset.y;
			thisPos = transform.position;
			thisPos.y = lookTarget.position.y + targetOffset.y;

			lookAtPos = (playerPos - thisPos) * 0.5f;

			transform.LookAt(thisPos + lookAtPos);
        }
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(thisPos + lookAtPos, 10);
	}

    public void LookAtMother() {
		Vector3 targetDir = ((nodeManage.transform.position +(Vector3.up*(100-distFromMother)*.1f)) - (lookTarget.position + targetOffset)).normalized;
        Vector3 lookDir = Vector3.Lerp(transform.forward, targetDir, Time.deltaTime);
        transform.LookAt(transform.position + lookDir);
		Vector3 dramaPos = (new Vector3(-lookDir.x, 0, -lookDir.z) * 30) + (lookTarget.position + targetOffset);
        transform.position = Vector3.Lerp(transform.position, dramaPos, followSpeed*Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour 
{
	public float followSpeed;
	public Transform cameraFollow;
	public Transform lookTarget;
    NodeManager nodeManage;
    float distFromMother;
	// Use this for initialization
	void Start () 
	{
		transform.position = cameraFollow.position;
		transform.LookAt(lookTarget.position);

        nodeManage = FindObjectOfType<NodeManager>();
	}

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
            transform.LookAt(lookTarget.position);
        }
	}

    public void LookAtMother() {
        Vector3 targetDir = ((nodeManage.transform.position +(Vector3.up*(100-distFromMother)*.1f)) - lookTarget.position).normalized;
        Vector3 lookDir = Vector3.Lerp(transform.forward, targetDir, Time.deltaTime);
        transform.LookAt(transform.position + lookDir);
        Vector3 dramaPos = (new Vector3(-lookDir.x, 0, -lookDir.z) * 30) + lookTarget.position;
        transform.position = Vector3.Lerp(transform.position, dramaPos, followSpeed*Time.deltaTime);
    }
}

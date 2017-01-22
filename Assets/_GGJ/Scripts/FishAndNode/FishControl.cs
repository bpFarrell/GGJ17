using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControl : MonoBehaviour {

    public float groupRadius = 5;
    public float fwdSpeed = 1;
    public float rotSpeed = 5;
    public float dotRight;
    public float dotFwd;
    public Vector3 testVec = Vector3.right;
    public bool test;
    public MeshRenderer mr;

    public Transform debugObj;
    // Use this for initialization
    void Start () {
        test = true;
        debugObj = GetComponent<FishBehavior>().target;
        SetColor(0);
	}
	
	// Update is called once per frame
	void Update () {
       
        transform.position += transform.forward * fwdSpeed * Time.deltaTime;

        
        if (test && debugObj!=null) {
            testVec = (debugObj.position - transform.position).normalized;
            //   Turn(testVec);
            LookTurn(testVec);
            if ((debugObj.position - transform.position).magnitude > groupRadius) {
                BabyComeBack(testVec);
            }
        }
    }
    void BabyComeBack(Vector3 dir) {
        dotFwd = Vector3.Dot(transform.forward, dir.normalized);
        float normDot = 1-((dotFwd + 1) * 0.5f);
        transform.Rotate(0, rotSpeed * normDot, 0);
    }
    void LookTurn(Vector3 dir) {
        dotFwd = Vector3.Dot(transform.forward, dir.normalized);
        dotFwd += 1;
        dotFwd *= 0.5f;
        Vector3 lookVec = Vector3.Lerp(transform.forward, dir.normalized, rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(lookVec,Vector3.up);
    }
    void Turn(Vector3 dir) {
        dotRight = Vector3.Dot(transform.right, dir.normalized);
        float r = 1;
        if (dotRight < 0) r = -1;
        float y = rotSpeed * Time.deltaTime * (1-Mathf.Abs( dotRight)) * r;
    //   if (!IsItRight(dir)) {
    //       y *= -1;
    //   }
        float yDx = transform.rotation.y + y;
        Quaternion rot = new Quaternion(transform.rotation.x, yDx, transform.rotation.z, transform.rotation.w);
        transform.rotation = rot;
    }

    bool IsItRight(Vector3 dir) {
        if ((dotRight = Vector3.Dot(transform.right, dir.normalized)) > 0) {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 0 is neutral 1 is P1 and 2 is P2
    /// </summary>
    /// <param name="player"></param>
    public void SetColor(int player) {
        if(player == 0) {
            mr.material = FishMaterials.Neutral;
        } else if (player == 1) {
            mr.material = FishMaterials.P1;
        }else if (player == 2) {
            mr.material = FishMaterials.P2;
        }
    }
}

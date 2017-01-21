using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

	static public Vector3 PointOnCircle(Vector3 origin, float angleInDegrees, float radius)
    {
        float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
        float z = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.z;
        return new Vector3(x, origin.y, z);
    }
}

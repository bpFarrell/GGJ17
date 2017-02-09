using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour {
    public float t = 0;
    Material mat;
    private void Awake() {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update () {
        mat.SetVector("_T", new Vector4(t, 0, 0, 0));
	}
}

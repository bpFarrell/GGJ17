using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
    [Range(0,1)]
    public float t;
    [Range(0, 1)]
    public float weight=0.5f;
    bool normalized= true;
    public float playSpeed = 1;
    public bool play = false;
    public Material mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        weight = GameInfo.main.fishRation;
	}
	
	// Update is called once per frame
	void Update () {
        if (play) {
            t = Mathf.Clamp01(t + Time.deltaTime * playSpeed);
            if(t == 1) {
                //t = 0;
                play = false;
            }
        }
        if (normalized) {
            mat.SetVector("_T", new Vector4((t * weight), ((t*0.9f+0.1f) * (1 - weight)), 0, 0));
        } else {
            //old
            mat.SetVector("_T", new Vector4(Mathf.Min(t,weight),Mathf.Min(t,1 - weight), 0, 0));
        }
	}
}

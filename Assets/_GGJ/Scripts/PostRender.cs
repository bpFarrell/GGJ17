using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRender : MonoBehaviour {
    public Material mat;
    void Awake() {
        mat = new Material(Shader.Find("Hidden/Post"));
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, mat);
    }
}

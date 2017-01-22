using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRender : MonoBehaviour {
    public Material mat;
    Camera cam;
    void Awake() {
        mat = new Material(Shader.Find("Hidden/Post"));
        cam = GetComponent<Camera>();
        cam.depthTextureMode |= DepthTextureMode.Depth;
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, mat);
    }
}

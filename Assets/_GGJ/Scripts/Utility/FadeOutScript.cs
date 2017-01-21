using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutScript : MonoBehaviour {

	public float timeToClear = 2.0f;
	
	private Image Image2Fade;
	
	// Use this for initialization
	void Start () {
		if (Image2Fade == null)
			Image2Fade = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		Color tempColor = Image2Fade.color;
		tempColor.a = tempColor.a - (Time.deltaTime / timeToClear);
		Image2Fade.color = tempColor;
		if(Image2Fade.color.a <= 0.0f)
		{
			tempColor.a = 0.0f;
			Image2Fade.color = tempColor;
			Destroy(this);
		}
	}
}

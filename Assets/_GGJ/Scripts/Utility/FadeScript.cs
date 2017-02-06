using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour 
{
	[SerializeField]
	private float _T = 2.0f;
	public float timeToFade
	{
		set
		{
			_T = value;
		}
	}
	public GameManager.simpleDelegate Callback = delegate {};
	[ReadOnly][SerializeField]
	private int _Direction = 1;
	[SerializeField]
	private bool _Fade = true;
	[SerializeField]
	private bool _Start = false;
	public bool toBlack
	{
		set
		{
			_Direction = (value) ? 1 : -1;
			_Fade = value;
			_Start = true;
		}
	}
	public bool onAwake = false;
	[ReadOnly][SerializeField]
	private Image Image2Fade;
	
	// Use this for initialization
	void Start () 
	{
		if (Image2Fade == null)
		{
			Image2Fade = GetComponent<Image>();
		}
		if (onAwake) toBlack = _Fade;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!_Start) return;

		Color tempColor = Image2Fade.color;
		tempColor.a = tempColor.a + ((Time.deltaTime / _T) * _Direction);
		Image2Fade.color = tempColor;
		//if(Image2Fade.color.a >= Mathf.Floor(_Direction))
		//{
		//	Callback();
		//	tempColor.a = 1.0f;
		//	Image2Fade.color = tempColor;
		//	Destroy(this);
		//}

		if (_Fade) 
		{
			if(Image2Fade.color.a >= 1.0f)
			{
				Callback();
				tempColor.a = 1.0f;
				Image2Fade.color = tempColor;
				Destroy(this);
			}
		}
		else
		{
			if(Image2Fade.color.a <= 0.0f)
			{
				Callback();
				tempColor.a = 0.0f;
				Image2Fade.color = tempColor;
				Destroy(this);
			}
		}
	}
}

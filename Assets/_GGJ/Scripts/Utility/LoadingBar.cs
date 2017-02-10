using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {
    public float t = 0.0f;
	public float fillTime = 5.0f;
	public float delay = 0.0f;
	float _T = 0.0f;
	Image img;
    Material mat;

    private void Awake() {
        //mat = GetComponent<MeshRenderer>().material;
		img = GetComponent<Image>(); 
		mat = img.material;

		GameStateMachine._Main.sLoading.StartOfState += loadAwake;
		GameStateMachine._Main.sLoading.UpdateState += LoadingUpdate;
		GameStateMachine._Main.sLoading.ExitState += loadAsleep;

		loadAsleep();
    }

    // Update is called once per frame
    void Update () {
        mat.SetVector("_T", new Vector4(t, 0, 0, 0));
	}

	void LoadingUpdate ()
	{
		_T += Time.deltaTime;
		if (_T < 0.0f) return;
		if (_T > fillTime + delay) 
		{
			GameStateMachine._Main.ChangeState(GameStateMachine.STATE.GAME); 
			return;
		}
		t = _T / (fillTime - delay);
	}

	void loadAsleep()
	{
		img.enabled = false;
		_T = delay;
		t = 0.0f;
	}

	void loadAwake()
	{
		img.enabled = true;
		_T = delay;
		t = 0.0f;
	}
}
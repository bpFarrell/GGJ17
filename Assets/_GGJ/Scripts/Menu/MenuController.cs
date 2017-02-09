using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour 
{
	static public MenuController _Main;

	public float loadingScreenWait = 5.0f;
	public GameObject fadePanel;
	public GameObject[] fadePanels;
	public GameObject[] loadingIcons;
	public GameObject[] titleScreens;
	public GameObject[] toyGroups;

	void Awake ()
	{
		if (!MenuController._Main)
			_Main = this;
		else
		{
			Debug.LogError("Multiple MenuController scripts have appeared in the scene.", gameObject);
			Destroy(this);
		}
		gameObject.transform.parent = null;
		GameStateMachine._Main.sLoading.StartOfState += LoadingStart;
		GameStateMachine._Main.sLoading.ExitState += EndOfLoading;
	}

	public void PressedPlay()
	{
		GameStateMachine._Main.ChangeState(GameStateMachine.STATE.LOADING);
	}

	public void LoadingStart()
	{
		// Fade to black
		FadeObject(fadePanels, 0.4f, true, LoadAssets);

		foreach (GameObject obj in titleScreens) 
		{
			obj.SetActive(false);
		}
		// Animate loading icons

		FadeObject(loadingIcons, 1.0f, true);
	}

	private void LoadAssets()
	{
		//TODO: Load in assets instead of scene
		SceneManager.LoadScene("_Arena", LoadSceneMode.Additive);
		wait(5.0f, LoadingDone );
	}

	private void LoadingDone()
	{
		GameStateMachine._Main.ChangeState(GameStateMachine.STATE.GAME);
	}

	private void EndOfLoading()
	{
		FadeObject(fadePanels, 0.4f, false);
		
		foreach (GameObject obj in loadingIcons) {
			obj.SetActive(false);
		}
		foreach (GameObject obj in toyGroups) {
			obj.SetActive(false);
		}
	}

	private void WaitedForLoading()
	{
		FadeOutObject(fadePanel, 0.4f);

		foreach (GameObject obj in loadingIcons) {
			obj.SetActive(false);
		}
		foreach (GameObject obj in toyGroups) {
			obj.SetActive(false);
		}
		// Load STATEMACHINE with some end of round functions.
		GameStateManager._MAIN.Reset.StartOfState += ResetGameMenu;
	}

	IEnumerator ExecuteAfterDelay(float time, GameManager.simpleDelegate dele)
	{
		yield return new WaitForSeconds(time);

		dele();
	}

	public void PressedQuit()
	{
		// Exit Application.
		Debug.Log ("Quit Pressed.");
		Application.Quit();
	}

	public void FadeInObject(GameObject obj, float duration)
	{
		FadeInScript tempFade = obj.AddComponent<FadeInScript>() as FadeInScript;
		tempFade.timeToOpaque = duration;
	}

	public void FadeOutObject(GameObject obj, float duration)
	{
		FadeOutScript tempFade = obj.AddComponent<FadeOutScript>() as FadeOutScript;
		tempFade.timeToClear = duration;
	}

	public FadeScript FadeObject(GameObject obj, float duration, bool toBlack, GameManager.simpleDelegate callback = null)
	{
		FadeScript tempFade = obj.AddComponent<FadeScript>() as FadeScript;
		tempFade.timeToFade = duration;
		tempFade.toBlack = toBlack;
		if (callback != null)tempFade.Callback = callback;
		return tempFade;
	}
	public void FadeObject(GameObject[] obj, float duration, bool toBlack, GameManager.simpleDelegate callback = null)
	{
		FadeScript tempFade = obj[0].AddComponent<FadeScript>() as FadeScript;;
		tempFade.timeToFade = duration;
		tempFade.toBlack = toBlack;
		if (callback != null)tempFade.Callback = callback;
		for (int i = 1; i < obj.Length; i++) 
		{
			tempFade = obj[i].AddComponent<FadeScript>() as FadeScript;
			tempFade.timeToFade = duration;
			tempFade.toBlack = toBlack;
		}
		return;
	}

	public WaitScript wait(float seconds, GameManager.simpleDelegate callback = null)
	{
		WaitScript script = gameObject.AddComponent<WaitScript>() as WaitScript;
		if(callback != null) script.Callback = callback;
		return script;
	}

	public void ResetGameMenu()
	{
		SceneManager.LoadScene("_Menu");
		//FadeInObject(fadePanel, 0.4f);
		//foreach (GameObject obj in loadingIcons) {
		//	obj.SetActive(true);
		//}
		//foreach (GameObject obj in toyGroups) {
		//	obj.SetActive(true);
		//}
		//voidFunction del = resetMid;
		//StartCoroutine(ExecuteAfterDelay(2.0f, del));
	}
	public void resetMid()
	{
		SceneManager.UnloadSceneAsync("_Arena");

		foreach (GameObject obj in titleScreens) {
			obj.SetActive(true);
		}

		GameManager.simpleDelegate del = resetEnd;
		StartCoroutine(ExecuteAfterDelay(2.0f, del));
	}

	public void resetEnd()
	{
		FadeOutObject(fadePanel, 0.4f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour 
{
	public float loadingScreenWait = 5.0f;
	public GameObject fadePanel;
	public GameObject[] loadingIcons;
	public GameObject[] titleScreens;
	public GameObject[] toyGroups;

	private float fadeCounter = 0.0f;
	private float fadeRate = 0.1f;

	// Use this for initialization
	void Start () 
	{
		fadeCounter = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("P1_Y"))
		{
			GameStateManager._MAIN.ChangeState(GameStateManager.STATE.RESET);
		}
	}

	public void PressedPlay()
	{
		Debug.Log ("Play Pressed.");
		// Fade to black
		FadeInObject(fadePanel, 0.4f);
		foreach (GameObject obj in titleScreens) {
			obj.SetActive(false);
		}
		// Animate loading icon
		foreach (GameObject obj in loadingIcons) {
			FadeInObject(obj, 1.0f);
			FadeInObject(obj, 1.0f);
		}

		// Wait arbitrary 5 seconds or so for loading screen
		voidFunction del = MidLoading;
		StartCoroutine(ExecuteAfterDelay(2.0f, del));
		// LoadSceneAdditively the game screen and start initializations
	}

	private void MidLoading()
	{
		SceneManager.LoadScene("_Arena", LoadSceneMode.Additive);

		voidFunction del = WaitedForLoading;
		StartCoroutine(ExecuteAfterDelay(2.0f, del));
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

	private delegate void voidFunction();
	IEnumerator ExecuteAfterDelay(float time, voidFunction dele)
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

		voidFunction del = resetEnd;
		StartCoroutine(ExecuteAfterDelay(2.0f, del));
	}

	public void resetEnd()
	{
		FadeOutObject(fadePanel, 0.4f);
	}
}

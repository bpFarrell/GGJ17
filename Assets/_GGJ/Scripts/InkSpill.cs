using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkSpill : MonoBehaviour 
{
	public string player = "1";
	public GameObject inkPrefab;
	public GameObject sputterPrefab;
	public float delay = 4.0f;
	public int numberOfInks = 3;
	public float delayBetweenInks = 0.7f;

	private string playerPrefix = "P1_";
	private float counter = 4.0f;
	private float sputCounter = 4.0f;

    public GameObject inkAudio;

	// Use this for initialization
	void Start () 
	{
		playerPrefix = "P" + player + "_";
		counter = delay;
		sputCounter = delay/8;
	}
	
	// Update is called once per frame
	void Update () 
	{
        //return;
		if (counter < delay) counter += Time.deltaTime;
		if (sputCounter < delay/8) sputCounter += Time.deltaTime;
		if (Input.GetButtonDown(playerPrefix + "X"))
		{
			if(counter >= delay)
			{
				for (int i = 0; i < numberOfInks; ++i) 
				{
					StartCoroutine(InkAfterDelay(delayBetweenInks * i));
				}
				counter = 0.0f;
				sputCounter = 0.0f;
                DropAudio();
			}
			else if (sputCounter >= delay/8) 
			{
				InkSputter();
				sputCounter = 0.0f;
			}
		}
	}

	IEnumerator InkAfterDelay(float time)
	{
		yield return new WaitForSeconds(time);

		DropInk();
	}

	void DropInk()
	{
		Instantiate (inkPrefab, transform.position, inkPrefab.transform.rotation);
	}

    void DropAudio()
    {
        Instantiate(inkAudio, transform.position, inkAudio.transform.rotation);
    }

    void InkSputter()
	{
		Instantiate (sputterPrefab, transform.position, sputterPrefab.transform.rotation);
	}
}

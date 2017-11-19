using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShhScreenShake : MonoBehaviour {

	[SerializeField] float shakeLenght = 1;
	[SerializeField] float shakePower = 0.5f;
	[SerializeField] float increasePower = 0.01f;
	[SerializeField] float increaseDuration = 0.05f;
	private Rect viewPort;
	bool gettingCloser;

	private void Awake()
	{
		viewPort = Camera.main.rect;
		int i = Random.Range(0, 1);
		if (i == 0)
		{
			StartCoroutine(Shake());
		}
	}


	IEnumerator Shake()
	{
		Debug.Log("Shake ");
		float t = 0;
		while (t < shakeLenght)
		{
			t += Time.deltaTime;
			Camera.main.rect = new Rect(0f + Random.Range(0f, shakePower), 0 + Random.Range(0f, shakePower), 1 - Random.Range(0f, shakePower), 1f - Random.Range(0f, shakePower));
			Handheld.Vibrate();
			yield return null;
		}
		if (gettingCloser)
		{
			shakePower += increasePower;
			shakeLenght += increaseDuration;
		}
		Camera.main.rect = viewPort;
	}


}
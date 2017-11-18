using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public List<AudioSource> sounds;

	private void Awake()
	{
		foreach (var sound in sounds)
		{
			Debug.Log(sound.gameObject.name);
			EventManager.StartListening(sound.gameObject.name,PlaySound);
		}
	}

	private void PlaySound(AudioSource inputSound)
	{
		Debug.Log(inputSound.name);
		inputSound.Play();
	}
}

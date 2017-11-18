using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	public AudioSource source;
	public List<AudioClip> clip;

	private void Awake()
	{
		source.volume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
	}

	public void VolumeChanged ()
	{
		source.volume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
	}

	private void PlaySound(AudioSource inputSound)
	{
		Debug.Log(inputSound.name);
		inputSound.Play();
	}
}

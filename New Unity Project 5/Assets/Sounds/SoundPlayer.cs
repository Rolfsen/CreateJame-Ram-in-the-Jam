using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	public AudioSource source;
	public List<AudioClip> clips;

	private void Awake()
	{
		source.volume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
	}

	public void VolumeChanged ()
	{
		source.volume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
		PlaySound(clips[5]);
	}

	public void PlaySound (AudioClip audioClip)
	{
		source.clip = audioClip;
		source.Play();
	}
}

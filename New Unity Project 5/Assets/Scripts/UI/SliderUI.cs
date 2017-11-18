using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SliderUI : MonoBehaviour
{
	public AudioMixer audio;
	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.minValue = -80f;
		slider.maxValue = 20.0f;
	}
/*
	private void OnEnable()
	{
		slider = GetComponent<Slider>();
		if (slider != null)
		{
			slider.normalizedValue = PlayerPrefs.HasKey("SoundVolume") ? PlayerPrefs.GetFloat("SoundVolume") : 0.5f;
		}
		else
		{
			Debug.LogError("Slider is: " + slider);
		}
	}*/


	public void UpdateSoundVolume()
	{
		PlayerPrefs.SetFloat("SoundVolume", slider.value);
		audio.SetFloat("Vol", slider.value);

	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{

	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
	}

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
	}


	public void UpdateSoundVolume()
	{
		PlayerPrefs.SetFloat("SoundVolume", slider.value);
	}
}

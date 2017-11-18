using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour {

	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();		
	}


	public void UpdateSoundVolume ()
	{
		PlayerPrefs.SetFloat("SoundVolume",slider.value);
	}

}

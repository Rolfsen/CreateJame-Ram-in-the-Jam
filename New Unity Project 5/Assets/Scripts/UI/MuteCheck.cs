using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteCheck : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("SoundMuted") == 1)

		{
			text.text = "Unmute Sound";
		}
		else
		{
			text.text = "Mute Sound";
		}
		Debug.Log(PlayerPrefs.GetInt("SoundMuted"));
	}
}

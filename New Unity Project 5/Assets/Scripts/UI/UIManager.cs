using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class UIManager: MonoBehaviour {


	[SerializeField]
	string sceneName;
	[SerializeField]
	GameObject mainMenu;
	[SerializeField]
	GameObject settingsMenu;
	[SerializeField]
	GameObject muteButton;
	[SerializeField]
	GameObject playGameButton;

	[SerializeField]
	private AudioMixer audioMixer;

	[SerializeField]
	AudioSource explotion;

	

	[SerializeField]
	EventSystem eventSystem;


	private void Start()
	{
		if (!PlayerPrefs.HasKey("SoundMuted"))
		{
			PlayerPrefs.SetInt("SoundMuted", 0);
		}
		if (!PlayerPrefs.HasKey("SoundVolume"))
		{
			PlayerPrefs.SetFloat("SoundVolume",0.5f);
		}
		audioMixer.SetFloat("Master", 0.5f);
	}

	public void GoToScene ()
	{
		explotion.Play();
		SceneManager.LoadScene(sceneName);
	}
	public void QuitGame ()
	{
		Application.Quit();
	}
	public void Settings ()
	{
		explotion.Play();
		mainMenu.SetActive(false);
		settingsMenu.SetActive(true);
		eventSystem.SetSelectedGameObject(muteButton);
	}
	public void BackToMain()
	{
		explotion.Play();
		mainMenu.SetActive(true);
		settingsMenu.SetActive(false);
		eventSystem.SetSelectedGameObject(playGameButton);
	}
	public void MuteSound ()
	{
		if (PlayerPrefs.GetInt("SoundMuted") == 1)
		{
			PlayerPrefs.SetInt("SoundMuted", 0);
		}
		else
		{
			PlayerPrefs.SetFloat("LastVolume", PlayerPrefs.GetFloat("SoundVolume", 0.5f));
			PlayerPrefs.SetInt("SoundMuted", 1);
		}
	}
}

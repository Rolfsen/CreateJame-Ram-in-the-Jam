using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour {

	[SerializeField]
	string sceneName;
	[SerializeField]
	GameObject mainMenu;

	public void GoToScene ()
	{
		SceneManager.LoadScene(sceneName);
	}
	public void QuitGame ()
	{
		Application.Quit();
	}
	public void Settings ()
	{
		mainMenu.SetActive(false);		
	}
}

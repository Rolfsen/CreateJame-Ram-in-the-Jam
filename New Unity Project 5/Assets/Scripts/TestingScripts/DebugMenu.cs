using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugMenu : MonoBehaviour {

	EventSystem eventSystem;

	GameObject currentObject;

	[SerializeField]
	AudioSource audioSource;

	private void Start()
	{
		eventSystem = GetComponent<EventSystem>();
		currentObject = eventSystem.currentSelectedGameObject;
		
	}

	private void Update()
	{
		if (currentObject != eventSystem.currentSelectedGameObject)
		{
			audioSource.Play();
		}
		currentObject = eventSystem.currentSelectedGameObject;
	}


}

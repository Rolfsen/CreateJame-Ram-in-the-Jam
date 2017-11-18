using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugMenu : MonoBehaviour {

	EventSystem eventSystem;

	private void Start()
	{
		eventSystem = GetComponent<EventSystem>();
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawWireCube(eventSystem.currentSelectedGameObject.transform.position,new Vector3 (100,100,100));
	}


}

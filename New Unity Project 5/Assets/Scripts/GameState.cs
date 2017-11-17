using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	public List<string> forceKeys = new List<string>(4);
	public string currentKey;

	private void Start()
	{
		GetNewKey();
	}

	public void GetNewKey()
	{
		int getKey = Random.Range(0,forceKeys.Count-1);
		currentKey = forceKeys[getKey];
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

	public int counter = 0;
	int lives = 2;

	private void Update()
	{
		if (counter == lives)
		{
			Destroy(gameObject);
		}
	}
}

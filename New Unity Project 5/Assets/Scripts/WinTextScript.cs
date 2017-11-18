using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextScript : MonoBehaviour {
    [SerializeField]
    private int min;
    [SerializeField]
    private int max;
    // Use this for initialization
    void Start () {
        Vector3 position = this.transform.position;
        position.x = UnityEngine.Random.Range(min, max);
        this.transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

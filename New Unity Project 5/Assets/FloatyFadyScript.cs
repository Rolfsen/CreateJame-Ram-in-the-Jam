using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatyFadyScript : MonoBehaviour {
    public float transparent = 1f;
    private float speedChange = 0.05f;
    public Vector3 dir;
	// Use this for initialization
	void Start () {
		
	}
    public void Reset()
    {;
        transparent = 1f;
    }
    // Update is called once per frame
    void Update () {
        Vector3 tmpPos = this.transform.position;
        tmpPos += dir;
        this.transform.position = tmpPos;
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, transparent);
        transparent -= speedChange;

    }
}

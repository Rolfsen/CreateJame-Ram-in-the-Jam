using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCamScript : MonoBehaviour {
    [SerializeField]
    public Transform lookTarget;
    [SerializeField]
    public Transform followTarget;

    private Vector3 positionOffset;
    // Use this for initialization
    void Start () {
        positionOffset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(lookTarget);
        transform.position = followTarget.transform.position + positionOffset;
    }
}

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
	public void SetWinner(int playerId)
    {
        switch (playerId)
        {
            case 1:
                Transform temp = followTarget;
                followTarget = lookTarget;
                lookTarget = temp;
                positionOffset.x = -positionOffset.x;
                break;
            case 2:
                break;
        }
    }
	// Update is called once per frame
	void Update () {
        transform.LookAt(lookTarget);
        transform.position = followTarget.transform.position + positionOffset;
    }
}

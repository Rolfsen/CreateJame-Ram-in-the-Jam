using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressingAnim : MonoBehaviour {
    [SerializeField]
    private GameObject AButton;
    [SerializeField]
    private GameObject BButton;
    [SerializeField]
    private GameObject XButton;
    [SerializeField]
    private GameObject YButton;
    // Use this for initialization
    void Start () {
		
	}
	public void SetActiveButton(string buttonName)
    {
        switch (buttonName)
        {
            case "A":

                break;
            case "B":

                break;
            case "X":

                break;
            case "Y":

                break;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

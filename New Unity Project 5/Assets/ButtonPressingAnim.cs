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
        HideAllButtons();

    }
    void HideAllButtons()
    {
        AButton.SetActive(false);
        BButton.SetActive(false);
        XButton.SetActive(false);
        YButton.SetActive(false);
    }
	public void SetActiveButton(string buttonName)
    {
        HideAllButtons();
        switch (buttonName)
        {
            case "A":
                AButton.SetActive(true);
                break;
            case "B":
                BButton.SetActive(true);
                break;
            case "X":
                XButton.SetActive(true);
                break;
            case "Y":
                YButton.SetActive(true);
                break;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

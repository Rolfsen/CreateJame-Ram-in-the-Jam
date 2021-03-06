﻿using System.Collections;
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
    [SerializeField]
    private FloatyFadyScript GoodJobArrow;
    [SerializeField]
    private FloatyFadyScript BadJobArrow;

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
    public void GoodButton(string buttonName)
    {
        switch (buttonName)
        {
            case "A":
                GoodJobArrow.transform.position = AButton.transform.position;
                GoodJobArrow.Reset();
                break;
            case "B":
                GoodJobArrow.transform.position = BButton.transform.position;
                GoodJobArrow.Reset();
                break;
            case "X":
                GoodJobArrow.transform.position = XButton.transform.position;
                GoodJobArrow.Reset();
                break;
            case "Y":
                GoodJobArrow.transform.position = YButton.transform.position;
                GoodJobArrow.Reset();
                break;
        }
    }
    public void BadButton(string buttonName)
    {
        switch (buttonName)
        {
            case "A":
                BadJobArrow.transform.position = AButton.transform.position;
                BadJobArrow.Reset();
                break;
            case "B":
                BadJobArrow.transform.position = BButton.transform.position;
                BadJobArrow.Reset();
                break;
            case "X":
                BadJobArrow.transform.position = XButton.transform.position;
                BadJobArrow.Reset();
                break;
            case "Y":
                BadJobArrow.transform.position = YButton.transform.position;
                BadJobArrow.Reset();
                break;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}

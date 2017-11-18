using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
    [Header("Player Scripts")]
    [SerializeField]
    private PlayerControl player1;
    [SerializeField]
    private PlayerControl player2;
    [Header("UI Objects")]
    [SerializeField]
    private Text player1powerText;
    [SerializeField]
    private Text player2powerText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        player1powerText.text = "Player 1 Power: " + player1.comboJuice;
        player2powerText.text = "Player 2 Power: " + player2.comboJuice;
    }
}

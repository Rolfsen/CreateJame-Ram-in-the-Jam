using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GamePhase
{
    PAUSED = 0,
    RUNNING_PHASE = 1,
    START_JUMP_PHASE = 2,
    JUMPING_PHASE = 3,
    ENDING_PHASE = 4
}
public class GameState : MonoBehaviour
{
	[SerializeField]
	Vector3 gravity;

	public List<string> forceKeys = new List<string>(4);
    public string player1Key;
    public string player2Key;
    [SerializeField]
    private GameObject runningPhaseUI;
    [SerializeField]
    private GameObject jumpingPhaseUI;
    [SerializeField]
    private Slider sliderPlayer1;
    [SerializeField]
    private Slider sliderPlayer2;
    [SerializeField]
    private GameObject JamObject;
    [SerializeField]
    public bool PlayersShareQTEKeys = false;

    private float p1Distance;
    private float p2Distance;
    [System.Serializable]
	struct PlayerData
	{
		public PlayerControl player;
		public float playerScore;
	}
	[SerializeField]
    PlayerData[] players;

    [System.Serializable]
    struct TextData
    {
        public Text text;
        public string TextName;
    }
    [SerializeField]
    List<TextData> TextFields;

    private bool player1ReachedCameraTrigger = false;
    private bool player2ReachedCameraTrigger = false;
    public void PlayerReachedCameraTrigger(PlayerControl player)
    {
        if (player.player == PlayerControl.PlayerID.player1)
        {
            // player 1 reached the trigger
            player1ReachedCameraTrigger = true;
        } else if (player.player == PlayerControl.PlayerID.player2)
        {
            // player 2 reached the trigger
            player2ReachedCameraTrigger = true;
        } else
        {
            Debug.Log("More than two players detected - or player id not set correctly");
        }
        if (player1ReachedCameraTrigger&& player2ReachedCameraTrigger)
        {
            StartJumpCameraTransition();
            
        }
    }

    private void StartJumpCameraTransition()
    {
        players[0].player.reduceComboForJump = true;
        players[1].player.reduceComboForJump = true;
        runningPhaseUI.SetActive(false);
        jumpingPhaseUI.SetActive(false);
        players[0].player.GetComponentInChildren<Camera>().enabled = false;
        players[1].player.GetComponentInChildren<Camera>().enabled = false;
    }

    private void Start()
	{
		Physics.gravity = gravity;
		GetNewKey(PlayerControl.PlayerID.player1);
        GetNewKey(PlayerControl.PlayerID.player2);

        p1Distance = Vector3.Distance(players[0].player.transform.position, JamObject.transform.position);
        p2Distance = Vector3.Distance(players[1].player.transform.position, JamObject.transform.position);

        Debug.Log("Distance p1 to jam is: " + p1Distance);
        Debug.Log("Distance p2 to jam is: " + p2Distance);
    }
    public Text GetTextObject(string name)
    {
        Text Result;
        Result = TextFields.Find(x=>x.TextName == name).text;
        return Result;
    }
	private void Update()
	{
        GetTextObject("Player1Speed").text = "Player 1 Jump Force: " + players[0].player.jumpForce;
        GetTextObject("Player2Speed").text = "Player 2 Jump Force: " + players[1].player.jumpForce;

        GetTextObject("Player1ComboJuice").text = "Player 1 Combo: " + players[0].player.comboJuice;
        GetTextObject("Player2ComboJuice").text = "Player 2 Combo: " + players[1].player.comboJuice;

        //sliderPlayer1.value = initialDistance/

        for (int i = 0; i < players.Length; i++)
		{
			if (players[i].player.transform.position.y > players[i].playerScore)
			{
				players[i].playerScore = players[i].player.transform.position.y;
			}
		}

	}
		
    public string GetCurrentKey(PlayerControl.PlayerID player)
    {
        switch (player)
        {
            case PlayerControl.PlayerID.player1:
                return player1Key;
            case PlayerControl.PlayerID.player2:
                return player2Key;
            default:
                //should never happen
                return "A";
        }
    }
	public void GetNewKey(PlayerControl.PlayerID player)
	{
        if (PlayersShareQTEKeys)
        {
            int getKey = UnityEngine.Random.Range(0, forceKeys.Count - 1);
            player1Key = forceKeys[getKey];
            player2Key = forceKeys[getKey];
            GetTextObject("Player1QTE").text = "Player 1 Button: " + forceKeys[getKey];
            GetTextObject("Player2QTE").text = "Player 2 Button: " + forceKeys[getKey];
        } else
        {
            if (player == PlayerControl.PlayerID.player1)
            {
                int getKey = UnityEngine.Random.Range(0, forceKeys.Count - 1);
                player1Key = forceKeys[getKey];
                GetTextObject("Player1QTE").text = "\"" + forceKeys[getKey]+"\"";
            }
            else if (player == PlayerControl.PlayerID.player2)
            {
                int getKey = UnityEngine.Random.Range(0, forceKeys.Count - 1);
                player2Key = forceKeys[getKey];
                GetTextObject("Player2QTE").text = "\"" + forceKeys[getKey]+"\"";
            }
        }
    }
}

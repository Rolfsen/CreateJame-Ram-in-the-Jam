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
    [SerializeField]
    public Font font;
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
    private bool player1Jumped = false;
    private bool player2Jumped = false;
    public int winningPlayer = 0;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject winCam;

    private bool hasAlreadyLost = false;

	[Header("Sounds")]
	[SerializeField]
	AudioSource explotion;
	[SerializeField]
	AudioSource bgMusic;
	[SerializeField]
	List<AudioSource> splatter;

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

        GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(runningPhaseUI.transform);

        Text myText = newGO.AddComponent<Text>();
        myText.font = font;
        myText.text = "Ta-dah!";
    }
    public Text GetTextObject(string name)
    {
        Text Result;
        Result = TextFields.Find(x=>x.TextName == name).text;
        return Result;
    }
	private void Update()
	{
        GetTextObject("Player1Speed").text = "Player 1 Jump Force: " + Math.Round(players[0].player.jumpForce,2);
        GetTextObject("Player2Speed").text = "Player 2 Jump Force: " + Math.Round(players[1].player.jumpForce,2);

        GetTextObject("Player1ComboJuice").text = "Player 1 Combo: " + Math.Round(players[0].player.comboJuice,2);
        GetTextObject("Player2ComboJuice").text = "Player 2 Combo: " + Math.Round(players[1].player.comboJuice,2);

        //sliderPlayer1.value = initialDistance/

        for (int i = 0; i < players.Length; i++)
		{
			if (players[i].player.transform.position.y > players[i].playerScore)
			{
				players[i].playerScore = players[i].player.transform.position.y;
			}
		}

	}
	public void InformOfJump(PlayerControl.PlayerID player)
    {
        switch (player)
        {
            case PlayerControl.PlayerID.player1:
                player1Jumped = true;
                break;
            case PlayerControl.PlayerID.player2:
                player2Jumped = true;
                break;

        }
        if (player1Jumped && player2Jumped)
        {
            float p1score = players[0].player.jumpPowerUsed;
            float p2score = players[1].player.jumpPowerUsed;
            if (p1score > p2score)
            {
                //p1 wins
                players[1].player.isLoser = true;
                winningPlayer = 1;
            } else
            {
                //p2 wins (shh in a draw p2 wins)
                players[0].player.isLoser = true;
                winningPlayer = 2;
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
    public void GameEndCinematic()
    {
        
		bgMusic.Stop();
		explotion.Play();
		int i = UnityEngine.Random.Range(0,splatter.Count);
		splatter[i].Play();
        jumpingPhaseUI.SetActive(true);
        if (winningPlayer == 1)
        {
            GetTextObject("Player1Wins").gameObject.SetActive(true);
            winCam.GetComponent<WinCamScript>().SetWinner(1);
        } else
        {
            GetTextObject("Player2Wins").gameObject.SetActive(true);
            winCam.GetComponent<WinCamScript>().SetWinner(2);
        }
        mainCam.GetComponent<Camera>().enabled = false;
        winCam.GetComponent<Camera>().enabled = true;
        if (hasAlreadyLost)
        {
            GetTextObject("Player1Wins").gameObject.SetActive(false);
            GetTextObject("Player2Wins").gameObject.SetActive(false);
            GetTextObject("JammedIt").gameObject.SetActive(true);
        }
        hasAlreadyLost = true;
    }
	public void GetNewKey(PlayerControl.PlayerID player)
	{
        if (PlayersShareQTEKeys)
        {
            int getKey = UnityEngine.Random.Range(0, forceKeys.Count);
            player1Key = forceKeys[getKey];
            player2Key = forceKeys[getKey];
            GetTextObject("Player1QTE").text = "\"" + forceKeys[getKey]+"\"";
            GetTextObject("Player2QTE").text = "\"" + forceKeys[getKey]+"\"";
        } else
        {
            if (player == PlayerControl.PlayerID.player1)
            {
                int getKey = UnityEngine.Random.Range(0, forceKeys.Count);
                player1Key = forceKeys[getKey];
                GetTextObject("Player1QTE").text = "\"" + forceKeys[getKey]+"\"";
            }
            else if (player == PlayerControl.PlayerID.player2)
            {
                int getKey = UnityEngine.Random.Range(0, forceKeys.Count);
                player2Key = forceKeys[getKey];
                GetTextObject("Player2QTE").text = "\"" + forceKeys[getKey]+"\"";
            }
        }
    }
}

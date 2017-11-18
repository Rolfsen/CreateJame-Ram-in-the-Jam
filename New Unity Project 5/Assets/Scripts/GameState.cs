using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	public string currentKey;

	[System.Serializable]
	struct PlayerData
	{
		public PlayerControl player;
		public float playerScore;
	}

	[SerializeField]
	PlayerData[] players;

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
            players[0].player.GetComponentInChildren<Camera>().enabled = false;
            players[1].player.GetComponentInChildren<Camera>().enabled = false;
        }
    }
	private void Start()
	{
		Physics.gravity = gravity;
		GetNewKey();
	}

	private void Update()
	{
		for (int i = 0; i < players.Length; i++)
		{
			if (players[i].player.transform.position.y > players[i].playerScore)
			{
				players[i].playerScore = players[i].player.transform.position.y;
			}
		}
	}
		

	public void GetNewKey()
	{
		int getKey = Random.Range(0, forceKeys.Count - 1);
		currentKey = forceKeys[getKey];
	}
}

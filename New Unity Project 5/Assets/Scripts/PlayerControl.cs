﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[Header("Increase Ram Force")]
	[SerializeField]
	private KeyCode AButton;
	[SerializeField]
	private KeyCode BButton;
	[SerializeField]
	private KeyCode XButton;
	[SerializeField]
	private KeyCode YButton;
	[Header("Attack Other Ram")]
	[SerializeField]
	private KeyCode attackButton0;
	[SerializeField]
	private KeyCode attackButton1;
	[SerializeField]
	private KeyCode attackButton2;
	[SerializeField]
	private KeyCode attackButton3;
	[Header("Dodge attacks")]
	[SerializeField]
	private KeyCode dodgeButton0;
	[SerializeField]
	private KeyCode dodgeButton1;
	[SerializeField]
	private KeyCode dodgeButton2;
	[SerializeField]
	private KeyCode dodgeButton3;

	[Header("Player Variables")]
	[SerializeField]
	public float jumpForce;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float maxHeight;
	[SerializeField]
	private float increaseForce;
	[SerializeField]
	Vector3 moveDir;
	[Header("Button mashing difficulty")]
	[SerializeField]
	private float additionPerPress = 1.7f;
	[SerializeField]
	private float reductionFactor = 1.0f;
    // To make life easier tools;
    [SerializeField] bool isUsingController;
    [Header("Game Controller")]
    [SerializeField] GameState mainGameController;
    public enum PlayerID { player1, player2 }
    public PlayerID player;

    private const float MAX_FORCE = 10.0f;
	public float comboJuice = 0.0f;
	Rigidbody rb;
	GameState gameState;
	bool isJumpDone;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		gameState = FindObjectOfType<GameState>();
        if (isUsingController)
        {
            if (player == PlayerID.player1)
            {
                AButton = KeyCode.Joystick1Button0;
                BButton = KeyCode.Joystick1Button1;
                XButton = KeyCode.Joystick1Button2;
                YButton = KeyCode.Joystick1Button3;
            }
            else if (player == PlayerID.player2)
            {
                AButton = KeyCode.Joystick2Button0;
                BButton = KeyCode.Joystick2Button1;
                XButton = KeyCode.Joystick2Button2;
                YButton = KeyCode.Joystick2Button3;
            }
        }
    }
	KeyCode GetCurrentPowerUpKey()
	{
		// This should eventually choose random keys to press every couple secs to allow for differnt buttons to mash
		return AButton;
	}
	// Update is called once per frame
	void Update()
	{
		if (comboJuice > 0.4f)
		{
			// to make it harder to keep at a high value but easy to keep above zero
			comboJuice -= (((comboJuice / MAX_FORCE) * (additionPerPress) / 8) + 0.05f) * reductionFactor;
		}
		else
		{
			comboJuice = 0;
		}
		if (Input.GetKeyDown(AButton) )
		{
            if (gameState.currentKey == "A")
            {
                CorrectKeyPress(AButton);
            } else
            {
                WrongKeyPress(AButton);
                Debug.Log("Wrong Key Pressed: A");
            }
		}
		if (Input.GetKeyDown(BButton))
		{
            if (gameState.currentKey == "B")
            {
                CorrectKeyPress(AButton);
            }
            else
            {
                WrongKeyPress(BButton);
                Debug.Log("Wrong Key Pressed: B");
            }
        }
		if (Input.GetKeyDown(XButton))
		{
            if (gameState.currentKey == "X")
            {
                CorrectKeyPress(AButton);
            }
            else
            {
                WrongKeyPress(XButton);
                Debug.Log("Wrong Key Pressed: X");
            }
        }
		if (Input.GetKeyDown(YButton))
		{
            if (gameState.currentKey == "Y")
            {
                CorrectKeyPress(AButton);
            }
            else
            {
                WrongKeyPress(YButton);
                Debug.Log("Wrong Key Pressed: Y");
            }
        }
		if (Input.GetKeyDown(GetCurrentPowerUpKey()))
		{
			if (comboJuice + additionPerPress < MAX_FORCE)
			{
				comboJuice += additionPerPress;
			}
			else
			{
				comboJuice = additionPerPress;
			}
		}
	}

    private void WrongKeyPress(KeyCode ButtonPressed)
    {
        
    }

    void CorrectKeyPress(KeyCode buttonPressed)
	{
		jumpForce += increaseForce;
		gameState.GetNewKey();
	}

	private void FixedUpdate()
	{
		rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case ("startJump"):
				StartJump(other);
				break;
			case ("endJump"):
				EndJump(other.transform.gameObject);
				break;
            case ("switchCameraToJump"):
                mainGameController.PlayerReachedCameraTrigger(this);
                Debug.Log("Hit Camera Trigger");
                break;
			default:
				Debug.LogWarning("Unknown tag: " + other.tag);
				break;

		}
	}

	void StartJump(Collider other)
	{
		Destroy(other.gameObject);
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
	}
	void EndJump(GameObject other)
	{
		if (!isJumpDone)
		{
			Debug.Log(Time.time);
			isJumpDone = true;
			other.GetComponent<Counter>().counter++;
		}
	}
}

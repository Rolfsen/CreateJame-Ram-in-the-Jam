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
    [SerializeField]
    private float comboFactor = 1.0f;
    public bool reduceComboForJump = false;
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
    public float jumpPowerUsed;
    private bool inJam=false;
    private bool isJumping=false;
    public bool isLoser=false;

	[Header("Sounds")]
	[SerializeField]
	List<AudioSource> glassCollision;
	[SerializeField]
	List<AudioSource> jump;
	[SerializeField]
	AudioSource Hooves;
	[SerializeField]
	AudioSource error;
	[SerializeField]
	AudioSource correctPress;

	[Header("Animations")]
	[SerializeField]
	List<GameObject> runningModels;
	[SerializeField]
	GameObject jumpingModels;
	[SerializeField]
	GameObject flyingAnimation;
	[SerializeField]
	GameObject victoryPos;
	[SerializeField]
	GameObject deathAnim;
    [SerializeField]
    private float increaseAmount;
    [SerializeField]
    private float jumpPhaseSpeed;
    GameObject currentModel;
    public bool isWinner=false;

    private void Start()
	{
		Debug.Log(Input.GetJoystickNames().Length);
		rb = GetComponent<Rigidbody>();
		gameState = FindObjectOfType<GameState>();
        if (Input.GetJoystickNames().Length >= 1)
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
		int i = UnityEngine.Random.Range(0,runningModels.Count);
		currentModel = runningModels[i];
		currentModel.SetActive(true);
        InvokeRepeating("SpeedUp", 1.0f, 1.0f);
    }
    public void SpeedUp()
    {
        if (inJam)
        {
            return;
        }
        if (isJumping)
        {
            return;
        }
        if (isLoser)
        {
            return;
        }
        if (isWinner)
        {
            return;
        }
            moveSpeed += increaseAmount;
            Debug.Log("Sped Up. Speed now: " + moveSpeed);
    }
	KeyCode GetCurrentPowerUpKey()
	{
		// This should eventually choose random keys to press every couple secs to allow for differnt buttons to mash
		return AButton;
	}
	// Update is called once per frame
	void Update()
	{
        if (!reduceComboForJump)
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
        }
        string CurrentKey = mainGameController.GetCurrentKey(player);

        if (Input.GetKeyDown(AButton) )
		{
            if (CurrentKey == "A")
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
            if (CurrentKey == "B")
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
            if (CurrentKey == "X")
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
            if (CurrentKey == "Y")
            {
                CorrectKeyPress(AButton);
            }
            else
            {
                WrongKeyPress(YButton);
                Debug.Log("Wrong Key Pressed: Y");
            }
        }

        if (isJumping && isLoser)
        {

            switch (player)
            {
                case PlayerID.player1:
                    if (this.transform.position.x > 0)
                    {
                        // we are over the jam... SLAM
                        this.moveSpeed = 0;
                        Vector3 TempVel = this.GetComponent<Rigidbody>().velocity;
                        TempVel.y *= -2;
                        TempVel.y = -Math.Abs(TempVel.y);
                        this.GetComponent<Rigidbody>().velocity = TempVel;
                        this.isJumping = false;
                        mainGameController.GameEndCinematic();
                        mainGameController.blockJam = true;
						flyingAnimation.SetActive(false);
						deathAnim.SetActive(true);
					}
                    break;
                case PlayerID.player2:
                    if (this.transform.position.x < 0)
                    {
                        // we are over the jam... SLAM
                        this.moveSpeed = 0;
                        Vector3 TempVel = this.GetComponent<Rigidbody>().velocity;
                        TempVel.y *= -2;
                        TempVel.y = -Math.Abs(TempVel.y);
                        this.GetComponent<Rigidbody>().velocity = TempVel;
                        this.isJumping = false;
                        mainGameController.GameEndCinematic();
                        mainGameController.blockJam = true;
						flyingAnimation.SetActive(false);
						deathAnim.SetActive(true);
					}
                    break;
            }
        }
	}

	public void Victory ()
	{
		currentModel.SetActive(false);
		flyingAnimation.SetActive(false);
		victoryPos.SetActive(true);
        isWinner = true;
	}

    private void WrongKeyPress(KeyCode ButtonPressed)
    {
		error.Play();
        comboJuice = 0;
        jumpForce -= increaseForce;
        string keyName = "A";
        if (ButtonPressed == AButton)
        {
                keyName = "A";
        }
        if (ButtonPressed == BButton)
        {
            keyName = "B";
        }
        if (ButtonPressed == XButton)
        { 
            keyName = "X";
        }
        if (ButtonPressed == YButton)
        {
            keyName = "Y";
        }
        mainGameController.BadKeyPress(keyName, player);
		// Bad things happen
    }

    void CorrectKeyPress(KeyCode buttonPressed)
	{
		correctPress.Play();
        string CurrentKey = mainGameController.GetCurrentKey(player);
        mainGameController.GoodPress(CurrentKey,player);

        jumpForce += increaseForce;
		gameState.GetNewKey(player);
        if (comboJuice + additionPerPress < MAX_FORCE)
        {
            comboJuice += additionPerPress;
        }
        else
        {
            comboJuice = MAX_FORCE;
        }
        
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
                moveSpeed = jumpPhaseSpeed;
                break;
            case ("loseTrigger"):
				// we are over the jam... SLAM
				flyingAnimation.SetActive(false);
				deathAnim.SetActive(true);
				this.moveSpeed = 0;
                Vector3 TempPos = this.GetComponent<Rigidbody>().transform.position;
                TempPos.x = 0;
                TempPos.y = 2;
                TempPos.z = 0;
                this.GetComponent<Rigidbody>().transform.position = TempPos;
                //TempPos.y = 0;
                //this.GetComponent<Rigidbody>().velocity = TempPos;
                inJam = true;
                this.isJumping = false;
                switch (player)
                {
                    case PlayerID.player1:
                        mainGameController.winningPlayer = 2;
                        break;
                    case PlayerID.player2:
                        mainGameController.winningPlayer = 1;
                        break;
                }
				victoryPos.SetActive(false);
                mainGameController.GameEndCinematic();
                break;
			case ("jam"):
				int i = UnityEngine.Random.Range(0, jump.Count - 1);
				glassCollision[i].Play();
				break;
			default:
				Debug.LogWarning("Unknown tag: " + other.tag);
				break;
		}
	}

	public void HideVictoryPose()
	{
		victoryPos.SetActive(false);
	}

	void StartJump(Collider other)
	{
		Hooves.Stop();
		currentModel.SetActive(false);
		flyingAnimation.SetActive(true);
		int i = UnityEngine.Random.Range(0,jump.Count-1);
		jump[i].Play();
		Destroy(other.gameObject);
        jumpPowerUsed = jumpForce + (comboJuice * comboFactor);
        rb.AddForce(new Vector3(0, jumpPowerUsed, 0), ForceMode.Impulse);
        Debug.Log("JUMP! used " + jumpForce + " of jump force and " + (comboJuice * comboFactor) + " of combo juice");
        isJumping = true;
        mainGameController.InformOfJump(player);
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

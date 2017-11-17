using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[Header("Increase Ram Force")]
	[SerializeField]
	private KeyCode powerUpButton0;
	[SerializeField]
	private KeyCode powerUpButton1;
	[SerializeField]
	private KeyCode powerUpButton2;
	[SerializeField]
	private KeyCode powerUpButton3;
	[Header("Attack Other Ram")]
	[SerializeField]
	private KeyCode attackButton0;
	[SerializeField]
	private KeyCode attackButton1;
	[SerializeField]
	private KeyCode attackButton2;
	[SerializeField]
	private KeyCode attackpButton3;
	[Header("Dodge attacks")]
	[SerializeField]
	private KeyCode dogdeButton0;
	[SerializeField]
	private KeyCode dogdeButton1;
	[SerializeField]
	private KeyCode dogdeButton2;
	[SerializeField]
	private KeyCode dogdepButton3;

	[Header("Player Variables")]
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float maxHeight;
	[SerializeField]
	private float increaseForce;
	[SerializeField]
	Vector3 moveDir;

	Rigidbody rb;
	GameState gameState;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		gameState = FindObjectOfType<GameState>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(powerUpButton0) && gameState.currentKey == "A")
		{
			CurrectKeyPress(powerUpButton0);
		}
		if (Input.GetKeyDown(powerUpButton1) && gameState.currentKey == "B")
		{
			CurrectKeyPress(powerUpButton1) ;
		}
		if (Input.GetKeyDown(powerUpButton2) && gameState.currentKey == "X")
		{
			CurrectKeyPress(powerUpButton2);
		}
		if (Input.GetKeyDown(powerUpButton3) && gameState.currentKey == "Y")
		{
			CurrectKeyPress(powerUpButton3);
		}
	}

	void CurrectKeyPress (KeyCode buttonPressed)
	{
		jumpForce += increaseForce;
		gameState.GetNewKey();
		Debug.Log("PowerUp Button" + buttonPressed + " pressed by: " + gameObject.name);
	}
	void WrongKeyPress ()
	{

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
		}
	}

	void StartJump(Collider other)
	{
		Destroy(other.gameObject);
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
	}
}

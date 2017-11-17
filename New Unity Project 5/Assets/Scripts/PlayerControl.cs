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

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(powerUpButton0))
		{
			jumpForce += increaseForce;
			Debug.Log("PowerUp Button0 pressed by: " + gameObject.name);
		}
		if (Input.GetKeyDown(powerUpButton1))
		{
			Debug.Log("PowerUp Button1 pressed by: " + gameObject.name);
		}
		if (Input.GetKeyDown(powerUpButton2))
		{
			Debug.Log("PowerUp Button2 pressed by: " + gameObject.name);
		}
		if (Input.GetKeyDown(powerUpButton3))
		{
			Debug.Log("PowerUp Button3 pressed by: " + gameObject.name);
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
				Debug.Log(other.tag);
				StartJump();
				break;
		}
	}

	void StartJump()
	{
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
	}
}

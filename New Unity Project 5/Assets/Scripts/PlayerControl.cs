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
    [Header("Button mashing difficulty")]
    [SerializeField]
    private float additionPerPress = 1.7f;
    [SerializeField] private float reductionFactor = 1.0f;

    private const float MAX_FORCE = 10.0f;
    public float currentForce = 0.0f;
    Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
    KeyCode GetCurrentPowerUpKey()
    {
        // This should eventually choose random keys to press every couple secs to allow for differnt buttons to mash
        return powerUpButton0;
    }
    // Update is called once per frame
    void Update()
	{
        if (currentForce > 0.4f)
        {
            // to make it harder to keep at a high value but easy to keep above zero
            currentForce -= (((currentForce / MAX_FORCE) * (additionPerPress) / 8) + 0.05f) * reductionFactor;
        }
        else
        {
            currentForce = 0;
        }
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
        if (Input.GetKeyDown(GetCurrentPowerUpKey()))
        {
            Debug.Log("Power up key pressed. Current force: " + currentForce);
            if (currentForce + additionPerPress < MAX_FORCE)
            {
                currentForce += additionPerPress;
            }
            else
            {
                currentForce = additionPerPress;
            }
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
		}
	}

	void StartJump(Collider other)
	{
		Destroy(other.gameObject);
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
	}
}

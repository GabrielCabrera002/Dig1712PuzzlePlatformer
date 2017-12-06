using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class PlayerGroundMovement
{
	#region Fields
	PlayerController player;
	PlayerMovement movementSupermodule;
	MovementControls controls;
	Vector3 newPos; // for deciding where to position the player each frame
	#endregion

	#region Properties
	[SerializeField]
	float moveSpeed = 5f;
	public bool isActive {get; set;}
	float speedStabilizer
	{
		get { return moveSpeed * Time.deltaTime;}
	}

	Vector3 playerPos
	{
		get { return player.transform.position; }
		set { player.transform.position = value; }
	}
	#endregion

	#region Methods
	public PlayerGroundMovement()
	{
		isActive = true;
	}

	public void Init(PlayerController player)
	{
		this.player = player;
		controls = MovementControls.instance;
	}
	public void Execute()
	{
		if (isActive)
		{
			newPos = playerPos;

			if (Input.GetKey(controls.moveLeft))
				newPos += Vector3.left * speedStabilizer;

			if (Input.GetKey(controls.moveRight))
				newPos += Vector3.right * speedStabilizer;

			if (Input.GetKey(controls.moveForward))
				newPos += Vector3.forward * speedStabilizer;

			if (Input.GetKey(controls.moveBackward))
				newPos += Vector3.back * speedStabilizer;

			playerPos = newPos;
		}
	}

	#endregion
}
[System.Serializable]
public class PlayerJumpMovement
{
	#region Fields
	
	PlayerMovement movementSupermodule;
	MovementControls controls;
	public Vector3 jumpForce = new Vector3(0f, 400f, 0f);

	[SerializeField]
	public int multiJump = 1;

	// Helps enforce a delay between jumps on the jumps.
	int timer = 0;
	float frameRate;
	[SerializeField]
	float delayBetweenJumps = 0.4f; // how long in seconds between each jump
	int timeBuffer; // How long in frames between each jump
	bool timerReady = true;
	bool hasJumpsToUse = true;
	bool pressedJumpKey = false;

	[Header("For debugging")]
	[SerializeField]
	int jumps = 0;
	#endregion

	#region Properties
	PlayerController player { get; set; }
	bool isActive { get; set; }
	
	#endregion
	
	#region Methods
	public PlayerJumpMovement()
	{
		isActive = true;
		controls = MovementControls.instance;
	}
	public void Init(PlayerController player)
	{
		this.player = player;
		SetupTimeValues();
		SetupCallbacks();
	}

	void SetupTimeValues()
	{
		frameRate = 1f / Time.deltaTime;
		timer = timeBuffer; // so the player can jump immediately
		timeBuffer = (int)(frameRate * delayBetweenJumps);
	}

	void SetupCallbacks()
	{
		player.mBEvents.OnCollisionEnter.AddListener(OnCollisionEnter);
		player.mBEvents.OnCollisionExit.AddListener(OnCollisionExit);
	}
	public void Execute()
	{
		if (isActive)
		{
			pressedJumpKey = Input.GetKeyDown(controls.jump);
			hasJumpsToUse = jumps < multiJump;
			timerReady = timer >= timeBuffer;

			if (pressedJumpKey && hasJumpsToUse && timerReady)
			{
				Debug.Log("Jumping!");
				player.rigidbody.AddForce(jumpForce);
				jumps++;
				timer = 0;
			}
			else if (!timerReady)
				// Let the timer only increment until it is ready. Keeps timer's value
				// from wrapping around the int limits.
				timer++;
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		// let the player jump again when they touch ground
		bool touchedTheGround = coll.gameObject.CompareTag("Ground");

		if (touchedTheGround)
		{
			Debug.Log("Touched the ground!");
			ResetJumps();
		}
	}
	void OnCollisionExit(Collision coll)
	{
		bool leftTheGround = coll.gameObject.CompareTag("Ground");

		if (leftTheGround)
		{
			Debug.Log("Left the ground!");
		}
		
	}

	void ResetJumps()
	{
		jumps = 0;
	}

	#endregion
}

[System.Serializable]
public class PlayerMovement
{
	#region Submodules
	[SerializeField]
	PlayerJumpMovement jump;

	[SerializeField]
	PlayerGroundMovement ground;
	#endregion

	#region Fields
	PlayerController player;
	
	#endregion

	#region Properties
	public bool isActive { get; set; }
	public MovementControls controls { get; set; }
	
	#endregion
	
	#region Methods
	public PlayerMovement()
	{
		controls = MovementControls.instance;
		isActive = true;
	}

	public void Init(PlayerController player)
	{
		this.player = player;
		jump.Init(player);
		ground.Init(player);
		//player.StartCoroutine(VerticalMotion());
	}

	public void Execute()
	{
		if (isActive)
		{
			ground.Execute();
			jump.Execute();
		}

	}

	#endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MW.Behaviour;

[RequireComponent(typeof(Helicopter))]
public class HelicopterMovement : MPlayer2D
{
	Helicopter Self;

	Camera MainCamera;
	float HelicopterXLength = 1;
	float HelicopterYHeight = 1;

	static readonly Vector3 RotateVector = new Vector3(0, 0, -15f);

	Vector3 VelLastFrame;
	Vector3 VelThisFrame;

	void Start()
	{
		Self = GetComponent<Helicopter>();
		Self.OnGameStatus += OnGameStatus;

		BoxCollider2D Collider = GetComponent<BoxCollider2D>();
		HelicopterXLength = Collider.size.x * .5f;
		HelicopterYHeight = Collider.size.y * .5f;

		MainCamera = Camera.main;
	}


	void Update()
	{
		HandleMovement();
	}

	void HandleMovement()
	{
		// If game is won or over, stop input.
		if (HasStoppedReceivingMovementInput())
			return;

		float HorizontalInput = Input.GetAxisRaw("Horizontal");
		float VerticalInput = Input.GetAxisRaw("Vertical");

		// Before actually moving, add the current position with the box collider boundaries and
		// check if it is outside of the screen. (WorldToScreenPoint).
		Vector3 ProjectedPosition = transform.position + new Vector3(HelicopterXLength * HorizontalInput, HelicopterYHeight * VerticalInput);
		Vector3 HelicopterWorldToScreen = MainCamera.WorldToScreenPoint(ProjectedPosition);

		// If the projected position of the player lies outside of the screen, stop
		// movement in that direction.
		if (HelicopterWorldToScreen.x < 0 || HelicopterWorldToScreen.x > Screen.width)
		{
			HorizontalInput = 0;
		}

		if (HelicopterWorldToScreen.y < 0 || HelicopterWorldToScreen.y > Screen.height)
		{
			VerticalInput = 0;
		}

		// Apply movement.
		MovementInput(VerticalInput, HorizontalInput);

		VelLastFrame = VelThisFrame;
		VelThisFrame = new Vector3(HorizontalInput, VerticalInput) * MovementSpeed;

		// Tilt the helicopter when moving horizontally.
		transform.localEulerAngles = RotateVector * HorizontalInput;
	}

	void OnGameStatus(bool bHasLost)
	{
		ReceiveMovementInput(true);
		transform.eulerAngles = Vector3.zero;
		GetComponent<Animator>().SetBool("bIsDowning", bHasLost);
	}

	public Vector3 GetVelocity()
	{
		return VelThisFrame;
	}

	public override void OnDestroy()
	{
		base.OnDestroy();

		Self.OnGameStatus -= OnGameStatus;
	}
}

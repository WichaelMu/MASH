using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MW.Behaviour;

[RequireComponent(typeof(Rigidbody2D), typeof(Helicopter))]
public class HelicopterMovement : MPlayer2D
{
	Helicopter Self;

	Camera MainCamera;
	float HelicopterXLength = 1;
	[MW.Editor.ReadOnly] float HelicopterYHeight = 1;

	static readonly Vector3 RotateVector = new Vector3(0, 0, -15f);

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
		if (HasStoppedReceivingMovementInput())
			return;

		float HorizontalInput = Input.GetAxisRaw("Horizontal");
		float VerticalInput = Input.GetAxisRaw("Vertical");

		Vector3 ProjectedPosition = transform.position + new Vector3(HelicopterXLength * HorizontalInput, HelicopterYHeight * VerticalInput);
		Vector3 HelicopterWorldToScreen = MainCamera.WorldToScreenPoint(ProjectedPosition);

		if (HelicopterWorldToScreen.x < 0 || HelicopterWorldToScreen.x > Screen.width)
		{
			HorizontalInput = 0;
		}

		if (HelicopterWorldToScreen.y < 0 || HelicopterWorldToScreen.y > Screen.height)
		{
			VerticalInput = 0;
		}

		MovementInput(VerticalInput, HorizontalInput);

		transform.localEulerAngles = RotateVector * HorizontalInput;
	}

	void OnGameStatus(bool bHasLost)
	{
		ReceiveMovementInput(true);
		transform.eulerAngles = Vector3.zero;
		GetComponent<Animator>().SetBool("bIsDowning", bHasLost);
	}

	public override void OnDestroy()
	{
		base.OnDestroy();

		Self.OnGameStatus -= OnGameStatus;
	}
}

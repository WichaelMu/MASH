using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Helicopter))]
public class HelicopterMovement : MonoBehaviour
{

	[SerializeField] float Speed;

	SpriteRenderer SpriteRenderer;
	Rigidbody2D Rigidbody;
	Vector3 Velocity;
	bool bStopControls = false;

	Helicopter Self;

	Camera MainCamera;
	float HelicopterXLength = 1;
	float HelicopterYHeight = 1;

	static readonly Vector3 RotateVector = new Vector3(0, 0, -15f);

	void Start()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
		SpriteRenderer = GetComponent<SpriteRenderer>();

		Self = GetComponent<Helicopter>();
		Self.OnGameStatus += OnGameStatus;

		BoxCollider2D Collider = GetComponent<BoxCollider2D>();
		HelicopterXLength = Collider.size.x * .5f;
		HelicopterYHeight = Collider.size.y * .5f;

		MainCamera = Camera.main;
	}


	void Update()
	{
		if (bStopControls)
			return;

		float HorizontalInput = Input.GetAxisRaw("Horizontal");
		float VerticalInput = Input.GetAxisRaw("Vertical");

		// Flip when going left. Ensure input is not zero. For some reason, don't put this condition in the assignment of flipX.
		if (HorizontalInput != 0)
			SpriteRenderer.flipX = HorizontalInput < 0;

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

		Velocity = new Vector3(HorizontalInput, VerticalInput).normalized;
		Velocity *= Speed;

		transform.localEulerAngles = RotateVector * HorizontalInput;
	}

	void FixedUpdate()
	{
		if (bStopControls)
			return;

		Rigidbody.MovePosition(transform.position + Velocity * Time.fixedDeltaTime);
	}

	void OnGameStatus(bool bHasLost)
	{
		bStopControls = true;
		transform.eulerAngles = Vector3.zero;
		GetComponent<Animator>().SetBool("bIsDowning", bHasLost);
	}

	void OnDestroy()
	{
		Self.OnGameStatus -= OnGameStatus;
	}
}

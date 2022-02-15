using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HelicopterMovement : MonoBehaviour
{

	[SerializeField] float Speed;

	SpriteRenderer SpriteRenderer;
	Rigidbody2D Rigidbody;
	Vector3 Velocity;
	Helicopter Self;
	bool bIsDead = false;

	Camera MainCamera;
	float HelicopterXLength = 1;
	float HelicopterYHeight = 1;

	void Start()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
		SpriteRenderer = GetComponent<SpriteRenderer>();

		Helicopter.OnGameStatus += (b) => bIsDead = b;

		BoxCollider2D Collider = GetComponent<BoxCollider2D>();
		(HelicopterXLength, HelicopterYHeight) = (Collider.size.x, Collider.size.y);

		MainCamera = Camera.main;
	}


	void Update()
	{
		if (bIsDead)
			return;

		float HorizontalInput = Input.GetAxisRaw("Horizontal");
		float VerticalInput = Input.GetAxisRaw("Vertical");

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
	}

	void FixedUpdate()
	{
		if (bIsDead)
			return;

		Rigidbody.MovePosition(transform.position + Velocity * Time.fixedDeltaTime);
	}
}

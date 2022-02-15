using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MW.IO;

[RequireComponent(typeof(Rigidbody2D))]
public class HelicopterMovement : MonoBehaviour
{

	[SerializeField] float Speed;

	Rigidbody2D Rigidbody;
	Vector3 Velocity;

	void Start()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
	}


	void Update()
	{
		Velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
		Velocity *= Speed * Time.deltaTime;
	}

	void FixedUpdate()
	{
		Rigidbody.MovePosition(transform.position + Velocity);
	}
}

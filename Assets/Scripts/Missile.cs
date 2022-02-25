using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MW.Kinetic;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{

	Rigidbody2D Self;
	Transform Target;
	float Speed, MaxDegreesDelta;

	public void Initialise(Transform Target, float Speed, float MaxDegreesDelta)
	{
		this.Speed = Speed;
		this.MaxDegreesDelta = MaxDegreesDelta;

		this.Target = Target;
		Self = GetComponent<Rigidbody2D>();

		Destroy(gameObject, 5);
	}

	void Update()
	{
		Kinematics.HomeTowards(Self, Target, Speed, MaxDegreesDelta);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Helicopter"))
		{
			Helicopter Helicopter = collision.GetComponent<Helicopter>();
			if (Helicopter)
			{
				Helicopter.GoDown();

				Destroy(gameObject);
			}
		}
	}
}

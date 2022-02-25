using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cannon : MonoBehaviour
{
	float CannonSpeed;

	const string kHelicopter = "Helicopter";

	public void Initialise(float CannonSpeed)
	{
		this.CannonSpeed = CannonSpeed;
		
		// Destroy after 20 seconds.
		Destroy(gameObject, 20);
	}
	
	void Update()
	{
		transform.position += transform.up * CannonSpeed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(kHelicopter))
		{
			Helicopter Helicopter = collision.GetComponent<Helicopter>();
			if (Helicopter)
				Helicopter.GoDown();
		}
	}
}

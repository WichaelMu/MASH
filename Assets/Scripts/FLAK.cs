using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FLAK : MonoBehaviour
{
	[SerializeField] Cannon Cannon;

	[SerializeField] HelicopterMovement Target;
	Rigidbody2D Self;

	[SerializeField] float CannonSpeed = 30;
	[SerializeField] float RoundsPerSecond = 5;

	void Start()
	{
		Self = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Vector3 Prediction = PredictiveProjectile();

		transform.up = (Prediction - transform.position).normalized;

		HandleShooting();
	}

	float LastFiredTime = 0;

	void HandleShooting()
	{
		if (Time.time - LastFiredTime > 1 / RoundsPerSecond)
		{
			Cannon c = Instantiate(Cannon, transform.position, transform.rotation);
			c.Initialise(CannonSpeed);

			LastFiredTime = Time.time;
		}
	}

	Vector3 PredictiveProjectile()
	{
		Vector3 TargetVelocity = Target.GetVelocity();

		if (TargetVelocity.magnitude < Vector3.kEpsilon)
		{
			return Target.transform.position;
		}

		float DistanceToHelicopter = Vector3.Distance(transform.position, Target.transform.position) * .001f;

		Vector3 ForwardPrediction = TargetVelocity * (1000f / CannonSpeed) * DistanceToHelicopter;

		return Target.transform.position + ForwardPrediction;
	}
}

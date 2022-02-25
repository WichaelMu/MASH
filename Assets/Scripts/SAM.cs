using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAM : MonoBehaviour
{
	[SerializeField] Missile Missile;
	[SerializeField] HelicopterMovement Target;

	[SerializeField] float MissileSpeed = 30;
	[SerializeField] float MissileTurnDegreesDelta = 15;
	[SerializeField] float SecondsBetweenLaunches = 5;

	float LastLaunchTime = 0;

	void Update()
	{
		if (Time.time - LastLaunchTime > SecondsBetweenLaunches)
		{
			Launch(MissileSpeed, MissileTurnDegreesDelta);

			LastLaunchTime = Time.time;
		}
	}

	public void Launch(float Speed, float MaxDegreesDelta)
	{
		Missile m = Instantiate(Missile, transform.position, Quaternion.identity);
		m.Initialise(Target.transform, Speed, MaxDegreesDelta);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPickup : MonoBehaviour
{

	uint SoldiersInHelicopter = 0;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (SoldiersInHelicopter < 3 && collision.CompareTag("Soldier"))
		{
			SoldiersInHelicopter++;
			Debug.Log("pickup");
		}
	}
}

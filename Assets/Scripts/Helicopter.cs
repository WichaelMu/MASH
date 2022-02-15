using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
	public static Action<bool> OnGameStatus;

	[SerializeField] TMPro.TextMeshProUGUI Counter;

	const string kRescured = "Soldiers Rescured: ";
	const string kInHeli = "Soldiers in Helicopter: ";

	uint SoldiersInHospital = 0;
	uint SoldiersInHelicopter = 0;

	int AllSoldiers = 0;

	void Start()
	{
		UpdateCounter();

		AllSoldiers = GameObject.FindGameObjectsWithTag("Soldier").Length;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (SoldiersInHelicopter < 3 && collision.CompareTag("Soldier"))
		{
			SoldiersInHelicopter++;
			Audio.AAudioInstance.Play("ONPICKUP");
		}

		if (collision.CompareTag("Hospital"))
		{
			SoldiersInHospital += SoldiersInHelicopter;
			SoldiersInHelicopter = 0;

			if (SoldiersInHospital == AllSoldiers)
			{
				DisplayGameStatus(false);
			}
		}

		if (collision.CompareTag("Tree"))
		{
			DisplayGameStatus(true);
		}

		UpdateCounter();
	}

	void UpdateCounter()
	{
		Counter.text = kRescured + SoldiersInHospital + "\n" + kInHeli + SoldiersInHelicopter;
	}

	void DisplayGameStatus(bool bIsGameOver)
	{
		OnGameStatus?.Invoke(bIsGameOver);
	}
}

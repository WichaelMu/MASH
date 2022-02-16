using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
	public Action<bool> OnGameStatus;

	[SerializeField] Transform InjuredSolidersHolder;
	[SerializeField] TMPro.TextMeshProUGUI Counter;

	const string kRescued = "Soldiers Rescued: ";
	const string kInHeli = "Soldiers in Helicopter: ";

	uint SoldiersInHospital = 0;
	uint SoldiersInHelicopter = 0;

	int AllSoldiers = 0;

	void Start()
	{
		UpdateCounter();

		AllSoldiers = GameObject.FindGameObjectsWithTag("Soldier").Length;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (SoldiersInHelicopter < 3 && c.CompareTag("Soldier"))
			PickupSolider(c.transform);

		if (c.CompareTag("Hospital"))
			DropAtHospital();

		if (c.CompareTag("Tree"))
			HitTree();

		UpdateCounter();
	}

	Vector3 SoliderInHeliScale = new Vector3(.2f, .2f, 1);

	void PickupSolider(Transform T)
	{
		T.localScale = SoliderInHeliScale;
		T.parent = InjuredSolidersHolder;
		T.localPosition = new Vector3(0, SoldiersInHelicopter * .15f);
		T.eulerAngles = InjuredSolidersHolder.eulerAngles;

		SoldiersInHelicopter++;
		Audio.AAudioInstance.Play("ONPICKUP", true);
	}

	void DropAtHospital()
	{
		SoldiersInHospital += SoldiersInHelicopter;
		SoldiersInHelicopter = 0;

		if (SoldiersInHospital == AllSoldiers)
		{
			DisplayGameStatus(false);
		}

		RemoveSoldiers();
	}

	void HitTree()
	{
		DisplayGameStatus(true);

		RemoveSoldiers();
	}

	void UpdateCounter()
	{
		Counter.text = kRescued + SoldiersInHospital + "\n" + kInHeli + SoldiersInHelicopter;
	}

	void DisplayGameStatus(bool bIsGameOver)
	{
		OnGameStatus?.Invoke(bIsGameOver);
	}

	void RemoveSoldiers()
	{
		foreach (Transform T in InjuredSolidersHolder)
		{
			Destroy(T.gameObject);
		}
	}
}

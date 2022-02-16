using System;
using System.Collections;
using UnityEngine;
using MW.Audio;
using MW.Diagnostics;
using MW.Easing;

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
		MAudio.AudioInstance.Play("ENGINE");
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
		Audio.AudioInstance.Play("ONPICKUP", true);
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

		StartCoroutine(PlayCrashingSound());
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

	IEnumerator PlayCrashingSound()
	{
		MAudio.AudioInstance.Stop("ENGINE");
		MAudio.AudioInstance.Play("CRASH");
		yield return new WaitForSeconds(.5f);
		MAudio.AudioInstance.Play("EXPLOSION");

		float t = 0;
		MSound Crash = MAudio.AudioInstance.Find("CRASH");
		float InitialVolume = Crash.fVolume;

		while (t <= 1)
		{
			t += Time.deltaTime;
			float Interpol = Interpolate.Linear(0, InitialVolume, t);
			Crash.fVolume = InitialVolume - Interpol;

			yield return null;
		}

		MAudio.AudioInstance.Stop("CRASH");
	}
}

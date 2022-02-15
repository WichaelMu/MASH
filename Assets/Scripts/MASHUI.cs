using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASHUI : MonoBehaviour
{

	[SerializeField] TMPro.TextMeshProUGUI GameStatus;

	void Start()
	{
		HelicopterPickup.OnGameStatus += UpdateGameUI;
	}

	void UpdateGameUI(bool bIsGameOver)
	{
		GameStatus.text = bIsGameOver ? "GAME OVER!" : "YOU WIN!";
	}

	void OnDestroy()
	{
		HelicopterPickup.OnGameStatus -= UpdateGameUI;
	}
}

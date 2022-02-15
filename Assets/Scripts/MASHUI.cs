using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MASHUI : MonoBehaviour
{

	[SerializeField] TMPro.TextMeshProUGUI GameStatus;

	void Start()
	{
		Helicopter.OnGameStatus += UpdateGameUI;
	}

	void UpdateGameUI(bool bIsGameOver)
	{
		GameStatus.text = bIsGameOver ? "GAME OVER!" : "YOU WIN!";
	}

	void OnDestroy()
	{
		Helicopter.OnGameStatus -= UpdateGameUI;
	}
}

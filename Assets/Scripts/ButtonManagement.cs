using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
	public void LoadIndex(int Index)
	{
		SceneManager.LoadScene(Index);
	}

	public void QuitGame()
	{
		Application.Quit();

#if UNITY_EDITOR
		Debug.Break();
		Debug.Log("Game has been ordered to quit!");
#endif
	}
}

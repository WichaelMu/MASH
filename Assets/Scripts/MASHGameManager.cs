using UnityEngine;
using UnityEngine.SceneManagement;
using MW.IO;

public class MASHGameManager : MonoBehaviour
{
	void Update()
	{
		if (I.Key(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (I.Key(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string mode;

	public void AI()
	{
		SceneManager.LoadScene("Asteroids");
	}

	public void Classic()
	{
		SceneManager.LoadScene("Asteroids Classic");
	}

	public void Coop()
	{
		SceneManager.LoadScene("Asteroids Coop");
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("Asteroids Menu");
	}

	public void Exit()
	{
		SceneManager.LoadScene("MainMenu");
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
	public void Normal()
	{
		SceneManager.LoadScene("Space Invaders");
	}

	public void Coop()
	{
		SceneManager.LoadScene("Space Invaders Coop");
	}

	public void AI()
	{
		SceneManager.LoadScene("Space Invaders AI");
	}

	public void vsAI()
	{
		SceneManager.LoadScene("Space Invaders vs AI");
	}

	public void pvp()
	{
		SceneManager.LoadScene("Space Invaders 1V1");
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

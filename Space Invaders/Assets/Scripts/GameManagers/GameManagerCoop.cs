using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerCoop : MonoBehaviour
{
	public Animator canvasAnim;
	public player1Coop player1;
	public player2 player2;
	public SpawnerCoop spawner;


	public bool gameOver;
	public bool player2Died;
	public bool player1Died;


	public Text gameOverText;

	public void GameOver()
	{
		if (gameOver == false)
		{
			gameOver = true;
			canvasAnim.Play("gameOver");
			player1.GameOver();
			player2.GameOver();
			spawner.GameOver();
		}
	}
	public void Win()
	{
		if (gameOver == false)
		{
			gameOver = true;
			gameOverText.text = "You Won";
			canvasAnim.Play("gameOver");
			player1.GameOver();
			player2.GameOver();
			spawner.GameOver();
		}
	}

	public void Player1Died()
	{
		player1Died = true;
	}

	public void Player2Died()
	{
		player2Died = true;
	}

	void Update()
	{
		if (player1Died && player2Died)
		{
			GameOver();
		}
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}

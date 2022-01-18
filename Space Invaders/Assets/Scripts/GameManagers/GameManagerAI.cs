using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAI : MonoBehaviour
{
	public Animator canvasAnim;
	public player1AI player1;
	public AI AI;
	public SpawnerAI spawner;


	public bool gameOver;
	public bool AIdied;
	public bool player1Died;



	public Text gameOverText;

	public void GameOver()
	{
		if (gameOver == false)
		{
			gameOver = true;
			canvasAnim.Play("gameOver");
			player1.GameOver();
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
			spawner.GameOver();
		}
	}

	public void Player1Died()
	{
		player1Died = true;
	}

	public void AIDied()
	{
		AIdied = true;
	}

	void Update()
	{
		if (player1Died && AIdied)
		{
			GameOver();
		}
	}
}

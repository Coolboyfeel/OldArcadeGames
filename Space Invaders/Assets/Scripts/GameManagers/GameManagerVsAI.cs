using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerVsAI : MonoBehaviour
{
	public Animator canvasAnim;
	public playerAIvsAI AI;
	public SpawnerVsAI spawner;


	public bool gameOver;



	public Text gameOverText;

	public void GameOver()
	{
		if (!gameOver)
		{
			gameOver = true;
			gameOverText.text = "The  AI  won";
			canvasAnim.Play("gameOver");
			spawner.GameOver();
			AI.GameOver();
		}
	}
	public void Win()
	{
		if (!gameOver)
		{
			gameOver = true;
			gameOverText.text = "You Won";
			canvasAnim.Play("gameOver");
			spawner.GameOver();
			AI.GameOver();
		}
	}
}

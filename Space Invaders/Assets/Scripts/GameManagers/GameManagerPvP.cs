using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerPvP : MonoBehaviour
{

	public Animator canvasAnim;
	public playerPvP player;
	public spawnerPvP spawner;


	public bool gameOver;
	public bool player1Died;


	public Text gameOverText;

	public void GameOver()
	{
		if (gameOver == false)
		{
			gameOver = true;
			gameOverText.text = "Invaders Won";
			canvasAnim.Play("gameOver");
			player.GameOver();
			spawner.GameOver();
		}
	}
	public void Win()
	{
		if (gameOver == false)
		{
			gameOver = true;
			gameOverText.text = "Player Won";
			canvasAnim.Play("gameOver");
			player.GameOver();
			spawner.GameOver();
		}
	}
}

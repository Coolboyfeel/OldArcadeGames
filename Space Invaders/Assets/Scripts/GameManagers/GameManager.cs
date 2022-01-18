using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator canvasAnim;
    public playerSpaceInvaders player;
    public InvaderSpawnerSpaceInvaders spawner;


	public bool gameOver;
	public bool player1Died;


	public Text gameOverText;

    public void GameOver() 
    {
        if (gameOver == false)
        {
            gameOver = true;
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
			gameOverText.text = "You Won";
			canvasAnim.Play("gameOver");
			player.GameOver();
			spawner.GameOver();
		}
	}

}

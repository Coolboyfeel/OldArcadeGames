using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring1V1 : MonoBehaviour
{
    public GameObject ball;
    public GameObject player1;
    public GameObject player2;
    public int playerScore;
    public int AIScore;
    public int finalScore;
    public Text playerText;
    public Text AiText;
    public Text gameOverText;
    public bool gameOver;
    public Animator anim;

    public void PlayerScored() 
    {
        playerScore++;

        if (playerScore < finalScore)
        {
            ball.GetComponent<ball1V1>().ResetPosition();
            player1.GetComponent<playerpaddlepvp>().ResetPosition();
            player2.GetComponent<playerpaddle2>().ResetPosition();
        }
        else
        {
            gameOver = true;
            ball.GetComponent<ball1V1>().ResetPosition();
            player1.GetComponent<playerpaddlepvp>().ResetPosition();
            player2.GetComponent<playerpaddle2>().ResetPosition();
            GameOver();
        }
    }

    public void ComputerScored()
    {
        AIScore++;

        if (AIScore < finalScore)
        {
            ball.GetComponent<ball1V1>().ResetPosition();
            player1.GetComponent<playerpaddlepvp>().ResetPosition();
            player2.GetComponent<playerpaddle2>().ResetPosition();
        }
        else
        {
            gameOver = true;
            ball.GetComponent<ball1V1>().ResetPosition();
            player1.GetComponent<playerpaddlepvp>().ResetPosition();
            player2.GetComponent<playerpaddle2>().ResetPosition();
            GameOver();
        }
    }

    void Update() 
    {
        playerText.text = playerScore.ToString();
        AiText.text = AIScore.ToString();
    }

    void GameOver()
    {
        if (playerScore == finalScore)
        {
            gameOverText.text = "PLAYER 1 WON!";
        }
        else if (AIScore == finalScore)
        {
            gameOverText.text = "PLAYER 2 WON!";
        }
        anim.Play("_won!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    public GameObject ball;
    public GameObject player1;
    public GameObject AI;
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

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ball.GetComponent<ball>().ResetPosition();
            player1.GetComponent<playerpaddle1>().ResetPosition();
            AI.GetComponent<computerpaddle>().ResetPosition();
        }
        else 
        {
            if (playerScore < finalScore)
            {
                ball.GetComponent<ball>().ResetPosition();
                player1.GetComponent<playerpaddle1>().ResetPosition();
                AI.GetComponent<computerpaddle>().ResetPosition();
            }
            else
            {
                gameOver = true;
                ball.GetComponent<ball>().ResetPosition();
                player1.GetComponent<playerpaddle1>().ResetPosition();
                AI.GetComponent<computerpaddle>().ResetPosition();
                GameOver();
            }
        }
        
    }

    public void ComputerScored()
    {
        AIScore++;

        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            ball.GetComponent<ball>().ResetPosition();
            player1.GetComponent<playerpaddle1>().ResetPosition();
            AI.GetComponent<computerpaddle>().ResetPosition();
        }
        else 
        {
            if (AIScore < finalScore)
            {
                ball.GetComponent<ball>().ResetPosition();
                player1.GetComponent<playerpaddle1>().ResetPosition();
                AI.GetComponent<computerpaddle>().ResetPosition();
            }
            else
            {
                gameOver = true;
                ball.GetComponent<ball>().ResetPosition();
                player1.GetComponent<playerpaddle1>().ResetPosition();
                AI.GetComponent<computerpaddle>().ResetPosition();
                GameOver();

            }
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
            gameOverText.text = "THE AI WON!";
        }
        anim.Play("_won!");
    }
}

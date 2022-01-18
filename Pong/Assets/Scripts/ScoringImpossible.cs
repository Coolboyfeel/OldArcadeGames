using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringImpossible : MonoBehaviour
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

        if (playerScore < finalScore)
        {
            ball.GetComponent<ballImpossible>().ResetPosition();
            player1.GetComponent<playerImpossible>().ResetPosition();
            AI.GetComponent<computerImpossible>().ResetPosition();
        }
        else
        {
            gameOver = true;
            ball.GetComponent<ballImpossible>().ResetPosition();
            player1.GetComponent<playerImpossible>().ResetPosition();
            AI.GetComponent<computerImpossible>().ResetPosition();
            GameOver();
        }
    }

    public void ComputerScored()
    {
        AIScore++;

        if (AIScore < finalScore)
        {
            ball.GetComponent<ballImpossible>().ResetPosition();
            player1.GetComponent<playerImpossible>().ResetPosition();
            AI.GetComponent<computerImpossible>().ResetPosition();
        }
        else
        {
            gameOver = true;
            ball.GetComponent<ballImpossible>().ResetPosition();
            player1.GetComponent<playerImpossible>().ResetPosition();
            AI.GetComponent<computerImpossible>().ResetPosition();
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
            gameOverText.text = "THE AI WON!";
        }
        anim.Play("_won!");
    }
}

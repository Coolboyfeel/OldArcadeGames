using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void AiEasy() 
    {
        SceneManager.LoadScene("AiEasy");
    }
    
    public void pvp() 
    {
        SceneManager.LoadScene("1V1");
    }

    public void AiMedium()
    {
        SceneManager.LoadScene("AiMedium");
    }

    public void AiHard()
    {
        SceneManager.LoadScene("AiHard");
    }

    public void AiImpossible()
    {
        SceneManager.LoadScene("AiImpossible");
    }

    public void BackToMenu() 
    {
        SceneManager.LoadScene("Pong Menu");
    }
    public void Exit() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}

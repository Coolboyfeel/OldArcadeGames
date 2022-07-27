using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ToggleControls() 
    {
        gameManager.ToggleKeys();
    }

    public void StartGame() 
    {
        gameManager.NewGame();
    }

    public void MainMenu() 
    {
        gameManager.BackToMenu();
    }
}

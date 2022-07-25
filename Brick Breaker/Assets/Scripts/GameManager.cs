using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    public TextMeshProUGUI scoreText {get; private set;}
    public TextMeshProUGUI livesText {get; private set;}
    public Ball ball {get; private set;}
    public Paddle paddle {get; private set;}
    public Brick[] bricks {get; private set;}
    public bool arrows = false;
    public bool speedUp = false; 
    public KeyCode [] activeKey;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        SceneManager.LoadScene("Main Menu");
    }

    

    public void NewGame() {
        this.score = 0;
        this.lives = 3;
        Time.timeScale = 1;

        LoadLevel(1);
    }

    private void Update() {
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (Time.timeScale == 1) 
            {
                Time.timeScale = 2;
            } else if (Time.timeScale == 2) {
                Time.timeScale = 5;
            } else if (Time.timeScale >= 5) 
            {
                Time.timeScale = 1;
            }
        }
    }

    private void LoadLevel(int level) 
    {
        this.level = level;
        SceneManager.LoadScene("Level" + level);
        Invoke("FindObjects", 0.1f);
    }

    private void FindObjects() 
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
    }
    private void ResetLevel() 
    {
        Time.timeScale = 1;
        ball.ResetBall();
        paddle.ResetPaddle();
    }
    private void GameOver() 
    {
        //SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void ToggleKeys() {
        if (arrows == false) 
        {
            arrows = true;
            activeKey[0] = KeyCode.LeftArrow;
            activeKey[1] = KeyCode.RightArrow;
        } else if (arrows == true)  {
            arrows = false;
            activeKey[0] = KeyCode.A;
            activeKey[1] = KeyCode.D;
        }      
    }

    public void Hit(Brick brick) 
    {
        score += brick.points;

        if(Cleared()) 
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared() 
    {
        for (int i = 0; i < this.bricks.Length; i++) 
        {
            if(bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) 
            {
                return false;
            }
        }

        return true; 
    }

    public void Miss() 
    {
        lives--;

        if(lives > 0) 
        {
            ResetLevel();
        } else {
            GameOver();
        }
    }
}


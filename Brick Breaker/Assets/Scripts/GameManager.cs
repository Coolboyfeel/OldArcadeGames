using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int totalLevels;
    public int score = 0;
    public int lives = 3;
    public float[] timers;
    public bool[] actives;
    public TextMeshProUGUI scoreText {get; private set;}
    public TextMeshProUGUI livesText {get; private set;}
    public TextMeshProUGUI[] timersTexts {get; private set;}
    public Ball[] ball;
    public Paddle paddle {get; private set;}
    public PowerUp[] powerUp {get; private set;}
    public GameObject[] bricks;
    public bool arrows = false;
    public bool speedUp = false; 
    public KeyCode [] activeKey;
    public Scene activeScene {get; private set;}

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        BackToMenu();
        //SceneManager.LoadScene(activeScene.name);
        //NewGame();
    }

    public void BackToMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }
    

    public void NewGame() {
        this.score = 0;
        this.lives = 3;
        Time.timeScale = 1;

        LoadLevel(1);
    }

    private void Update() {
        activeScene = SceneManager.GetActiveScene();
        if (scoreText != null && livesText != null) 
        {
            powerUp = FindObjectsOfType<PowerUp>();
            ball = FindObjectsOfType<Ball>();
            
            scoreText.enabled = true; scoreText.text = score.ToString();
            livesText.enabled = true; livesText.text = lives.ToString();
            if(Input.GetKeyDown(KeyCode.E) && ball[0].canChangeSpeed) 
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


            for (int i = 0; i < timersTexts.Length; i++) 
            {
                if(timers[i] > 0) {
                    timersTexts[i].gameObject.SetActive(true);
                    if(Mathf.RoundToInt(timers[i]) >= 10) 
                    {
                        timersTexts[i].text = "0:" + Mathf.RoundToInt(timers[i]).ToString();
                    } else if(Mathf.RoundToInt(timers[i]) < 10 && timers[i] > 0) {
                        timersTexts[i].text = "0:0" + Mathf.RoundToInt(timers[i]).ToString();
                    }
                    
                }
                else {
                    timersTexts[i].gameObject.SetActive(false);
                }
            }
        }

        for(int i = 0; i < timers.Length; i++) 
        {
            timers[i] -= Time.deltaTime;
        }

    }

    private void LoadLevel(int level) 
    {
        this.level = level;
        Time.timeScale = 1;
        if(totalLevels <= 4) 
        {
            SceneManager.LoadScene("Level" + level);
        } else {
            SceneManager.LoadScene("Won");
        }
        for(int i = 0; i < timers.Length; i++) 
        {
            timers[i] = 0;
            actives[i] = false;
        }
        Invoke("FindObjects", 0.1f);
    }

    private void FindObjects() 
    {
        ball = FindObjectsOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        timersTexts = GameObject.Find("TimersText").GetComponentsInChildren<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
    }
    private void ResetLevel() 
    {
        Time.timeScale = 1;
        //ball[CheckForPlace()].ResetBall();
        ball[0].ResetBall();
        paddle.ResetPaddle();
        for(int i = 0; i < powerUp.Length; i++) 
        {
            Destroy(powerUp[i].gameObject);
        }
        for(int i = 0; i < timers.Length; i++) 
        {
            timers[i] = 0;
            actives[i] = false;
        }
    }
    private void GameOver() 
    {
        //SceneManager.LoadScene("GameOver");

        SceneManager.LoadScene("Main Menu");
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

        StartCoroutine(CheckIfCleared());
    }

    IEnumerator CheckIfCleared() 
    {
        yield return new WaitForSeconds(0.5f);
        if(Cleared()) 
        {
            Debug.Log("Saw that its cleared");
            LoadLevel(this.level + 1);
        }
    }
    public bool Cleared() 
    {
        for (int i = 0; i < this.bricks.Length; i++) 
        {
            if(bricks[i].activeInHierarchy && !bricks[i].GetComponent<Brick>().unbreakable) 
            {
                Debug.Log("Not Cleared");
                return false;
            }
        }
        Debug.Log("Cleared");
        return true; 
    }

    public void Miss(Ball currentBall) 
    {
        if(ExtraBalls()) 
        {
            Destroy(currentBall.gameObject);
            if(!CheckForBall()) 
            {
                ChangeBall();
            }

        } else {
            lives--;
            if(lives > 0) 
            {
                ResetLevel();
            } else {
                GameOver();
            }   
        }
    }

    private bool ExtraBalls() 
    {
        if(ball.Length > 1) 
        {
            return true;
        } else {
            return false;
        }
    }

    private bool CheckForBall() 
    {
        for(int i = 0; i < ball.Length; i++) 
        {
            if(ball[i].gameObject.name == "Ball") 
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeBall() 
    {
        int randomNum = Random.Range(0, ball.Length);
        ball[randomNum].gameObject.name = "Ball";
        ball[randomNum].isMulti = false;
    }
    public void AddLife() 
    {
        lives++;
    }
}


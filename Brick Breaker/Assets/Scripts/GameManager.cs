using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int startLevel;

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

    [Header("Powerup Timers")]
    public float longTimer;
    public float shortTimer;
    public float slowTimer;
    public float fastTimer;
    public float inverseTimer;
    public float catchTimer;
    public float rewindTimer;

    [Header("Powerups Durations")]
    public float longDuration;
    public float shortDuration;
    public float slowDuration;
    public float fastDuration;
    public float inverseDuration;
    public float catchDuration;
    public float rewindDuration;

    [Header("Bools for Powerup")]
    public bool longActive = false;
    public bool shortActive = false;
    public bool slowActive = false;
    public bool fastActive = false;
    public bool inverseActive = false;
    public bool catchActive = false;
    public bool rewindActive = false;  

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        ResetAllPowerUps();
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

        LoadLevel(startLevel);
    }

    private void Update() {
        activeScene = SceneManager.GetActiveScene();
        if (scoreText != null && livesText != null) 
        {
            powerUp = FindObjectsOfType<PowerUp>();
            ball = FindObjectsOfType<Ball>();
            
            scoreText.enabled = true; scoreText.text = score.ToString();
            livesText.enabled = true; livesText.text = lives.ToString();
            //if(Input.GetKeyDown(KeyCode.Space) && ball[0].canChangeSpeed) 
            //{
                //if (Time.timeScale == 1) 
                //{
                    //Time.timeScale = 2;
                //} else if (Time.timeScale == 2) {
                    //Time.timeScale = 5;
                //} else if (Time.timeScale >= 5) 
                //{
                    //Time.timeScale = 1;
                //}
            //}

            UpdateTimersText();
            
        }

        UpdatePowerups();
    }

    private void LoadLevel(int level) 
    {
        this.level = level;
        ResetAllPowerUps();
        Time.timeScale = 1;
        if(totalLevels <= 4) 
        {
            SceneManager.LoadScene("Level" + level);
        } else {
            SceneManager.LoadScene("Won");
        }
        Invoke("FindObjects", 0.05f);

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
        ResetAllPowerUps();
        Time.timeScale = 1;
        //ball[CheckForPlace()].ResetBall();
        ball[0].ResetBall();
        paddle.ResetPaddle();
        for(int i = 0; i < powerUp.Length; i++) 
        {
            Destroy(powerUp[i].gameObject);
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

    public void Hit(Brick brick, int points) 
    {
        score += points;

        StartCoroutine(CheckIfCleared());
    }

    IEnumerator CheckIfCleared() 
    {
        yield return new WaitForSeconds(0.5f);
        if(Cleared()) 
        {
            LoadLevel(this.level + 1);
        }
    }
    public bool Cleared() 
    {
        for (int i = 0; i < this.bricks.Length; i++) 
        {
            Brick brick = bricks[i].GetComponent<Brick>();
            if(brick.sr.enabled && !brick.unbreakable) 
            {
                return false;
            }
        }
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

    public void ResetAllPowerUps() 
    {
        longTimer = 0;
        shortTimer = 0;
        slowTimer = 0;
        fastTimer = 0;
        inverseTimer = 0;
        catchTimer = 0;
        rewindTimer = 0;

        longActive = false;
        shortActive = false;
        slowActive = false;
        fastActive = false;
        inverseActive = false;
        catchActive = false;
        rewindActive = false;
    }

    public void UpdatePowerups() 
    {
        if(!rewindActive) {
            longTimer -= Time.deltaTime;
            shortTimer -= Time.deltaTime;
            slowTimer -= Time.deltaTime;
            fastTimer -= Time.deltaTime;
            inverseTimer -= Time.deltaTime;
            catchTimer -= Time.deltaTime;
        }  else {
            longTimer += Time.deltaTime;
            shortTimer += Time.deltaTime;
            slowTimer += Time.deltaTime;
            fastTimer += Time.deltaTime;
            inverseTimer += Time.deltaTime;
            catchTimer += Time.deltaTime;       
        }

        rewindTimer -= Time.deltaTime;

        if(longTimer < longDuration && longTimer > 0) {
            longActive = true;
        } else if(longTimer > longDuration) { longTimer = 0; longActive = false;}
        else {longActive = false;}

        if(shortTimer < shortDuration && shortTimer > 0) {
            shortActive = true;
        } else if(shortTimer > shortDuration) {shortTimer = 0; shortActive = false;}
        else {shortActive = false;} 

        if(slowTimer < slowDuration && slowTimer > 0) {
            slowActive = true;
        } else if(slowTimer > slowDuration) {slowTimer = 0; shortActive = false;}
        else {slowActive = false;}

        if(fastTimer < fastDuration && fastTimer > 0) {
            fastActive = true;
        } else if(fastTimer > fastDuration) {fastTimer = 0; fastActive = false;}
        else {fastActive = false;}

        if(inverseTimer < inverseDuration && inverseTimer > 0) {
            inverseActive = true;
        } else if(inverseTimer < inverseDuration) {inverseTimer = 0; inverseActive = false;} 
        else {inverseActive = false;}

        if(catchTimer < catchDuration && catchTimer > 0) {
            catchActive = true;
        } else if(catchTimer > catchDuration) {catchTimer = 0; catchActive = false;}
        else { catchActive = false;}

        if(rewindTimer < rewindDuration && rewindTimer > 0) {
            rewindActive = true;
        } else { rewindActive = false;}
 
        timers[0] = longTimer;
        timers[1] = shortTimer;
        timers[2] = slowTimer;
        timers[3] = fastTimer;
        timers[4] = inverseTimer;
        timers[5] = catchTimer;
        timers[6] = rewindTimer;

        actives[0] = longActive;
        actives[1] = shortActive;
        actives[2] = slowActive;
        actives[3] = fastActive;
        actives[4] = inverseActive;
        actives[5] = catchActive;
        actives[6] = rewindActive;       
    }

    public void UpdateTimersText() {
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
}
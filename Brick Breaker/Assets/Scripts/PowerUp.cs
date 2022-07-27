using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string[] powerUps;
    public float[] durations;
    public int multiBallCount;
    public int randomNum;
    public int points = 50;
    public float fallSpeed;
    public GameManager gameManager {get; private set;}
    public Paddle paddle {get; private set;}
    public SpriteRenderer sr {get; private set;}
    
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        paddle = FindObjectOfType<Paddle>();
        sr = GetComponent<SpriteRenderer>();
        randomNum = Random.Range(0, powerUps.Length);
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -10, 0f), fallSpeed * Time.deltaTime);
    }

    private void NewPowerUp(){
        randomNum = Random.Range(0, powerUps.Length);
        DoPowerUp();
     }

    private void OnTriggerEnter2D(Collider2D other) {   

        if(other.gameObject.tag == "Paddle") 
        {
            DoPowerUp();
        }
        if(other.gameObject.tag == "MissZone") {
            Destroy(this.gameObject);
        }
    }

    public void DoPowerUp() 
    {
       
        gameManager.score += points;
        sr.enabled = false;
        if(powerUps[randomNum] == "Life") 
        {
            gameManager.AddLife();
        } else if (powerUps[randomNum] == "Long") {
            if(gameManager.actives[5]) 
            {
                NewPowerUp();
                return;
            }
            gameManager.timers[0] = durations[0];
            paddle.StopCoroutine("Long");
            paddle.StartCoroutine("Long", durations[0]);

        } else if (powerUps[randomNum] == "Multi") {
            if(gameManager.ball.Length == 4) 
            {
                NewPowerUp();
                return;
            }
            gameManager.ball[0].Multi((multiBallCount + 1) - gameManager.ball.Length);
        } else if(powerUps[randomNum] == "Slow") 
        {
            if(gameManager.actives[2]) 
            {
                NewPowerUp();
                return;
            } 
            gameManager.timers[1] = durations[1];
            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Slow");
                gameManager.ball[i].StartCoroutine("Slow", durations[1]);
            }
        } else if(powerUps[randomNum] == "Fast") 
        {
            if(gameManager.actives[1]) 
            {
                NewPowerUp();
                return;
            }
            gameManager.timers[2] = durations[2];
            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Fast");              
                gameManager.ball[i].StartCoroutine("Fast", durations[1]);
            }
        } else if (powerUps[randomNum] == "Inverse") {
            gameManager.timers[3] = durations[3];
            paddle.StopCoroutine("Inverse");
            paddle.StartCoroutine("Inverse", durations[3]);
        } else if (powerUps[randomNum] == "Catch") {
            gameManager.timers[4] = durations[4];
            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Catch");              
                gameManager.ball[i].StartCoroutine("Catch", durations[4]);
            }
        } else if (powerUps[randomNum] == "Short") 
        {
            if(gameManager.actives[0]) 
            {
                NewPowerUp();
                return;
            }
            gameManager.timers[5] = durations[5];
            paddle.StopCoroutine("Short");
            paddle.StartCoroutine("Short", durations[5]);
        }
        Destroy(this.gameObject, 0.1f);
        
    }
}

   

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
    public int lazerAmmo;
    public float fallSpeed;
    public GameManager gameManager {get; private set;}
    public Paddle paddle {get; private set;}
    public SpriteRenderer sr {get; private set;}
    public CircleCollider2D cc {get; private set;}
    List<Vector3> positions; 

    [Header("Powerup durations")]
    public float longDuration;
    public float shortDuration;
    public float slowDuration;
    public float fastDuration;
    public float inverseDuration;
    public float catchDuration;
    public float rewindDuration;

    [Header("Audio")]
    public FMODUnity.EventReference lifeEvent;
    public FMODUnity.EventReference longEvent;
    public FMODUnity.EventReference shortEvent;

    public FMOD.Studio.EventInstance lifeAudio;
    public FMOD.Studio.EventInstance longAudio;
    public FMOD.Studio.EventInstance shortAudio;
    
    private void Start() {
        positions = new List<Vector3>();
        gameManager = FindObjectOfType<GameManager>();
        paddle = FindObjectOfType<Paddle>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        randomNum = Random.Range(0, powerUps.Length);
    }

    private void Update() {
        if(!gameManager.rewindActive) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -11, 0f), fallSpeed * Time.deltaTime);
        }

        if(transform.position.y >= paddle.gameObject.transform.position.y) 
        {
            cc.enabled = true;
        }
        
    }

    private void FixedUpdate() {
        if(gameManager.rewindActive) 
        {
            sr.enabled = true;
            if(positions.Count > 0) {
                transform.position = positions[0];
                positions.RemoveAt(0);
            }  else {
                Destroy(this.gameObject);
            }
        } else {
            cc.enabled = true;
            positions.Insert(0, transform.position);
        }

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
            StartCoroutine(Break());
        }
    }

    public void DoPowerUp() 
    {
       
        gameManager.score += points;
        sr.enabled = false;

        //Life Powerup
        if(powerUps[randomNum] == "Life") 
        {
            lifeAudio = FMODUnity.RuntimeManager.CreateInstance(lifeEvent);
            lifeAudio.start();
            gameManager.AddLife();
        } 
        
        //Long Powerup
        else if (powerUps[randomNum] == "Long") {
            if(gameManager.shortActive) 
            {
                NewPowerUp();
                return;
            }

            if(!gameManager.longActive) {
                longAudio = FMODUnity.RuntimeManager.CreateInstance(longEvent);
                longAudio.start();
            }
            

            gameManager.longTimer = longDuration;
            paddle.StopCoroutine("Long");
            paddle.StartCoroutine("Long", longDuration);

        } 
        
        //Multi Powerup
        else if (powerUps[randomNum] == "Multi") {
            if(gameManager.ball.Length >= 4) 
            {
                NewPowerUp();
                return;
            }
            gameManager.ball[0].Multi((multiBallCount + 1) - gameManager.ball.Length);
        } 
        
        //Slow Powerup
        else if(powerUps[randomNum] == "Slow") 
        {
            if(gameManager.fastActive) 
            {
                NewPowerUp();
                return;
            } 
            gameManager.slowTimer = slowDuration;

            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Slow");
                gameManager.ball[i].StartCoroutine("Slow", slowDuration);
            }
        } 
        
        //Fast Powerup
        else if(powerUps[randomNum] == "Fast") 
        {
            if(gameManager.slowActive) 
            {
                NewPowerUp();
                return;
            }
            gameManager.fastTimer = fastDuration;

            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Fast");              
                gameManager.ball[i].StartCoroutine("Fast", fastDuration);
            }
        } 
        
        //Inverse Powerup
        else if (powerUps[randomNum] == "Inverse") {
            gameManager.inverseTimer = inverseDuration;
            paddle.StopCoroutine("Inverse");
            paddle.StartCoroutine("Inverse", inverseDuration);
        } 
        
        //Catch Powerup
        else if (powerUps[randomNum] == "Catch") {
            
            gameManager.catchTimer = catchDuration;

            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Catch");              
                gameManager.ball[i].StartCoroutine("Catch", catchDuration);
            }
        } 
        
        //Short Powerup
        else if (powerUps[randomNum] == "Short") 
        {
            if(gameManager.longActive) 
            {
                NewPowerUp();
                return;
            }

            if(!gameManager.shortActive) {
                shortAudio = FMODUnity.RuntimeManager.CreateInstance(shortEvent);
                shortAudio.start();
            }
            gameManager.shortTimer = shortDuration;
            paddle.StopCoroutine("Short");
            paddle.StartCoroutine("Short", shortDuration);
        } 
        
        //Lazer Powerup
        else if (powerUps[randomNum] == "Lazer") {
            paddle.Lazer(lazerAmmo);
        }

        //Rewind Powerup
        else if(powerUps[randomNum] == "Rewind") 
        {
            if(gameManager.slowActive || gameManager.fastActive) 
            {
                NewPowerUp();
                return;
            }
            gameManager.rewindTimer = rewindDuration;
            
            for(int i = 0; i < gameManager.ball.Length; i++) 
            {
                gameManager.ball[i].StopCoroutine("Rewind");              
                gameManager.ball[i].StartCoroutine("Rewind", rewindDuration);
            }
            
        }

        Destroy(this.gameObject, 0.1f);      
    }


    IEnumerator Break() 
    {
        sr.enabled = false;
        cc.enabled = true;
          
        yield return new WaitForSeconds(rewindDuration);
        if(sr.enabled == false) {
            Destroy(this.gameObject);
        }  
    }
}



   

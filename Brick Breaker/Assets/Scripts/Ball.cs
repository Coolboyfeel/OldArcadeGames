using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool canChangeSpeed = true;
    public bool isMulti;
    private bool onPaddle;
    public float lastSpeed {get; private set;}
    public float multiplier;
    public float catchOffSet = 0.6f;
    public float slowedMultiplier;
    public float fastMultiplier;
    public Rigidbody2D rb {get; private set;}
    public float speed = 10f;
    public GameManager gameManager  {get; private set;}
    public GameObject multiBall;
    private float startSpeed = 10f;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        if(!isMulti) 
        {
            Invoke(("SetRandomTrajectory"), 1f);
        } else {
            SetRandomTrajectory();
        } 

        startSpeed = speed;
        lastSpeed = speed;
    }

    public void ResetBall() 
    {
        gameObject.name = "Ball";
        this.isMulti = false;
        canChangeSpeed = true;
        speed = startSpeed;
        transform.position = new Vector2(0, -3);
        rb.velocity = Vector2.zero;
        Invoke(("SetRandomTrajectory"), 1f); 
    }

    private void Update() 
    {
        if(!gameManager.actives[1]) 
        {
            speed = lastSpeed;
            lastSpeed = speed;
        }
        if(gameManager.ball.Length == 1) 
        {
            this.isMulti = false;
            this.gameObject.name = "Ball";
        }
        if(gameManager.actives[4]) 
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {  
                Vector2 force = Vector2.zero;
                force.x = Random.Range(-1f, 1f);
                force.y= 1f;
                onPaddle = false;
                gameManager.paddle.occupied = false;
                rb.AddForce(force * this.speed);    
            }
            if(onPaddle) {
                GameObject paddle = gameManager.paddle.gameObject;
                this.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + catchOffSet, 0);
            }
        }
    }
    private void FixedUpdate()
    {
        if(!gameManager.actives[1] && !gameManager.actives[2]) 
        {
            rb.velocity = rb.velocity.normalized * speed;
        } else if (gameManager.actives[1]) {
            rb.velocity = rb.velocity.normalized * (speed / slowedMultiplier);
        } else if (gameManager.actives[2]) {
            rb.velocity = rb.velocity.normalized * (speed * fastMultiplier);
        }
    }    

    private void SetRandomTrajectory() 
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        if(!isMulti) 
        {
            force.y= -1f;
 
        } else {
            force.y= 1f;
        }
        rb.AddForce(force * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Paddle" && gameManager.actives[1] == false) 
        {
            if(!gameManager.actives[4]) 
            {
                speed = speed * multiplier;
            }
            else if(gameManager.actives[4] && other.gameObject.GetComponent<Paddle>().occupied == false) {
                Catched(other.gameObject);
            }           
        }

        if(other.gameObject.tag == "MissZone") {
                gameManager.Miss(this);
            }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "BallCheck") 
        {
            if(Time.timeScale > 1) 
            {
                Time.timeScale = 1;
            }   
        }
        else if (other.gameObject.tag == "SpeedCheck") 
        {
            if(canChangeSpeed) 
            {
                canChangeSpeed = false;
            } else if (!canChangeSpeed) {
                canChangeSpeed = true;
            }
        }
    }

    private void Catched(GameObject obj) 
    {
        onPaddle = true;
        obj.GetComponent<Paddle>().occupied = true;
        this.speed = 0;  
    }

    public void Multi(int count) 
    {
        for (int i = 0; i < count; i++) 
        {
            var newMultibal = Instantiate(multiBall, this.transform.position, Quaternion.identity);
            newMultibal.name = "MultiBall";
            newMultibal.GetComponent<Ball>().isMulti = true; 
        } 
    }

    IEnumerator Slow(int duration) 
    {        
        if(!gameManager.actives[1]) 
        {
            Time.timeScale = 1;
            canChangeSpeed = false;
            gameManager.actives[1] = true;       
        }
        yield return new WaitForSeconds(duration);
        gameManager.actives[1] = false;
        canChangeSpeed = true;        
    }

    IEnumerator Fast(int duration) 
    {        
        if(!gameManager.actives[2]) 
        {
            Time.timeScale = 1;
            canChangeSpeed = false;
            gameManager.actives[2] = true;       
        }
        yield return new WaitForSeconds(duration);
        gameManager.actives[2] = false;
        canChangeSpeed = true;    
    }

    IEnumerator Catch(int duration) 
    {
        if(!gameManager.actives[4]) 
        {
            gameManager.actives[4] = true;       
        }
        yield return new WaitForSeconds(duration);
        gameManager.actives[4] = false;
        if(onPaddle) 
        {
            Vector2 force = Vector2.zero;
            force.x = Random.Range(-1f, 1f);
            force.y= 1f;
            onPaddle = false;
            gameManager.paddle.occupied = false;
            rb.AddForce(force * this.speed);
        }
    }
}
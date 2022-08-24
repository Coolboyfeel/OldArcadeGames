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
    
    public Rigidbody2D rb {get; private set;}
    public CircleCollider2D cc {get; private set;}
    public SpriteRenderer sr {get; private set;}
    public float rewindTime = 10f;
    public float speed = 10f;
    public GameManager gameManager  {get; private set;}
    
    private TrailRenderer tr;
    private float startSpeed = 10f;

    [Header("Powerup Stuff")]
    public float slowedMultiplier;
    public float fastMultiplier;
    public GameObject multiBall;
    List<Vector3> positions;
    private Vector2 rewindVelocity;

    [Header("Audio")]
    public FMODUnity.EventReference brickHitEvent;
    public FMODUnity.EventReference paddleHitEvent;
    public FMODUnity.EventReference wallHitEvent;
    public FMODUnity.EventReference unbreakableHitEvent;
    public FMOD.Studio.EventInstance brickHit;
    public FMOD.Studio.EventInstance paddleHit;
    public FMOD.Studio.EventInstance wallHit;
    public FMOD.Studio.EventInstance unbreakableHit;

    

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        positions = new List<Vector3>();

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
        positions = new List<Vector3>();
        Invoke(("SetRandomTrajectory"), 1f); 
    }

    private void Update() 
    {
        if(!gameManager.slowActive) 
        {
            speed = lastSpeed;
            lastSpeed = speed;
        }
        if(gameManager.ball.Length == 1) 
        {
            this.isMulti = false;
            this.gameObject.name = "Ball";
        }
        if(gameManager.catchActive) 
        {
            if(Input.GetKeyDown(KeyCode.E)) 
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

    void StartRewind() {
        rewindVelocity = rb.velocity;
    } 

    void StopRewind() {
        rb.velocity = rewindVelocity;    
    }

    private void FixedUpdate()
    {
        if(!gameManager.slowActive && !gameManager.fastActive) 
        {
            rb.velocity = rb.velocity.normalized * speed;
        } else if (gameManager.slowActive) {
            rb.velocity = rb.velocity.normalized * (speed / slowedMultiplier);
        } else if (gameManager.fastActive) {
            rb.velocity = rb.velocity.normalized * (speed * fastMultiplier);
        }

        if(gameManager.rewindActive) 
        {
            if(positions.Count > 0) {
                transform.position = positions[0];
                positions.RemoveAt(0);
            } else if(!isMulti) {
                gameManager.rewindActive = false;
            } else if(isMulti) {
                Destroy(this.gameObject);
            }
        } else {
            positions.Insert(0, transform.position);
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
        if (other.gameObject.tag == "Paddle") 
        {
            paddleHit = FMODUnity.RuntimeManager.CreateInstance(paddleHitEvent);
            paddleHit.start();

            if(!gameManager.catchActive) 
            {
                speed = speed * multiplier;
            }
            else if(gameManager.catchActive && other.gameObject.GetComponent<Paddle>().occupied == false) {
                Catched(other.gameObject);
            }           
        }
        else if (other.gameObject.tag == "Brick") 
        {
            if(other.gameObject.GetComponent<Brick>().unbreakable == false) 
            {
                brickHit = FMODUnity.RuntimeManager.CreateInstance(brickHitEvent);
                brickHit.start();
            }
            

            else{
                unbreakableHit = FMODUnity.RuntimeManager.CreateInstance(unbreakableHitEvent);
                unbreakableHit.start();
            }

        } else if(other.gameObject.tag == "Wall") 
        {
            wallHit = FMODUnity.RuntimeManager.CreateInstance(wallHitEvent);
            wallHit.start();
        }

        if(other.gameObject.tag == "MissZone") {
                gameManager.Miss(this);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameManager.slowActive || gameManager.fastActive) 
        {
            return;
        }
        
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
        Time.timeScale = 1;
        canChangeSpeed = false;
        gameManager.slowActive = true;

        yield return new WaitForSeconds(duration);
        gameManager.slowActive = false;
        canChangeSpeed = true;        
    }

    IEnumerator Fast(int duration) 
    {        
        Time.timeScale = 1;
        canChangeSpeed = false;
        gameManager.fastActive = true; 

        yield return new WaitForSeconds(duration);
        gameManager.fastActive = false;
        canChangeSpeed = true;    
    }

    IEnumerator Catch(int duration) 
    {
        if(!gameManager.catchActive) 
        {
            gameManager.catchActive = true;       
        }
        yield return new WaitForSeconds(duration);
        gameManager.catchActive = false;
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

    IEnumerator Rewind(int duration) 
    { 
        sr.enabled = true;
        cc.enabled = true;
        if(!gameManager.rewindActive) 
        {
            rewindVelocity = rb.velocity;
            gameManager.rewindActive = true;
        }
        yield return new WaitForSeconds(duration);
        rewindVelocity = rb.velocity;
        gameManager.rewindActive = false;
    }

    IEnumerator Break() 
    {
        sr.enabled = false;
        cc.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        transform.position = new Vector3(transform.position.x, -11f, 0f);
        yield return new WaitForSeconds(rewindTime);
        if(sr.enabled == false) {
            Destroy(this.gameObject);
        }  
    }
}
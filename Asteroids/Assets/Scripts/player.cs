using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private float thrusting;
    public float thrustspeed;
    public float turnSpeed;
    public GameObject bulletPrefab; 
    private float turnDirection;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    public bool gameOver;
    private bool playedGameOver;
	private GameManagerAstroids GameManager;
	public float lives;

	public float shootDelay;
	public bool shooting;

    void Awake()
    {
		GameManager = FindObjectOfType<GameManagerAstroids>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
	{
        thrusting = Input.GetAxis("Vertical");

		turnDirection = Input.GetAxis("Horizontal");
        
        if (gameOver == false) 
        {
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Shoot();
			}
			else if (Input.GetAxis("Fire1") > 0f)
			{
				if (!shooting)
				{
					Shoot();
				}
			}

        }
    }


    void FixedUpdate()
    {
        if (gameOver == false) 
        {
            if (thrusting > 0f)
            {
                rb.AddForce(this.transform.up * this.thrustspeed);
            }  
        }
        if (turnDirection > 0.0f || turnDirection < 0.0f)
        {
            rb.AddTorque(-turnDirection * this.turnSpeed);
        }
    }

    public void GameOver() 
    {
		gameOver = true;
        rb.gravityScale = 1;
        bc.isTrigger = true;
	}
    void Shoot()
    {
		shooting = true;
        Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
		Invoke("ShootFalse", shootDelay);
    }

	void ShootFalse()
	{
		shooting = false;
	}

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Asteroid") 
        {
			anim.Play("hitPlayer");
		}
		else if (other.gameObject.tag == "Asteroid2")
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = 0f;
		}
    }
}

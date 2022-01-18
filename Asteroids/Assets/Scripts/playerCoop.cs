using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCoop : MonoBehaviour
{
	private bool thrusting;
	public float thrustspeed;
	public float turnSpeed;
	public GameObject bulletPrefab;
	private float turnDirection;
	private Rigidbody2D rb;
	private BoxCollider2D bc;
	private Animator anim;
	public bool gameOver;
	public Animator animCanvas;
	private bool playedGameOver;
	private GameManagerAstroids GameManager;

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
		thrusting = Input.GetKey(KeyCode.UpArrow);

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			turnDirection = 1.0f;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			turnDirection = -1.0f;
		}
		else
		{
			turnDirection = 0.0f;
		}

		if (gameOver == false)
		{
			if (Input.GetKeyDown(KeyCode.Return))
				Shoot();
		}
	}


	void FixedUpdate()
	{
		if (gameOver == false)
		{
			if (thrusting)
			{
				rb.AddForce(this.transform.up * this.thrustspeed);
			}
		}
		if (turnDirection > 0.0f || turnDirection < 0.0f)
		{
			rb.AddTorque(turnDirection * this.turnSpeed);
		}
	}

	public void GameOver()
	{
		rb.gravityScale = 1;
		bc.isTrigger = true;
	}

	void Shoot()
	{
		Instantiate(this.bulletPrefab, this.transform.position, 
			this.transform.rotation);
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

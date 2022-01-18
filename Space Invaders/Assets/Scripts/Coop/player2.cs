using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
	public bulletSpaceInvaders bulletPrefab;
	private Vector2 direction;
	public float speed;
	private Rigidbody2D rb;
	public bool shooting;
	public Animator anim;
	private BoxCollider2D bc;
	public bool gameOver;
	public GameManagerCoop gameManager;
	public float gameOverDelay;

	private bool laserActive;
	void Awake()
	{
		bc = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction = new Vector2(-1f, 0f);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			direction = new Vector2(1f, 0f);
		}
		else
		{
			direction = new Vector2(0f, 0f);
		}

		if (gameOver == false)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				Shoot();
			}
		}
	}

	void FixedUpdate()
	{
		if (gameOver == false)
		{
			if (direction.sqrMagnitude > 0 || direction.sqrMagnitude < 0)
			{
				rb.AddForce(direction * this.speed);
			}
		}

	}

	void Shoot()
	{
		if (!laserActive)
		{
			bulletSpaceInvaders bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
			bullet.destroyed += BulletDestroyed;
			laserActive = true;
		}
	}

	public void BulletDestroyed()
	{
		laserActive = false;
	}

	public void GameOver()
	{
		gameOver = true;
		bc.enabled = false;
		rb.velocity = new Vector2(0f, 0f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameOver == false)
		{
			if (other.gameObject.tag == "Missile")
			{
				GameOver();
				anim.Play("PlayerHit");
				gameManager.Invoke("Player2Died", gameOverDelay);
			}
		}
	}
}

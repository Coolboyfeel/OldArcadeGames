using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAIvsAI : MonoBehaviour
{
	public bulletSpaceInvaders bulletPrefab;
	private Vector2 direction;
	public float speed;
	private Rigidbody2D rb;
	public bool shooting;
	public Animator anim;
	private BoxCollider2D bc;
	public bool gameOver;
	public GameManagerVsAI gameManager;
	public float gameOverDelay;

	public float pickTarget;
	public int willShoot;
	public float shootDelay;

	public GameObject targetParent;
	private Transform targetTrans;

	private float myTransformX;
	private float targetTransformX;

	private bool laserActive;

	void Awake()
	{
		InvokeRepeating("PickNewTarget", 0f, pickTarget);
		InvokeRepeating("ShootRandom", 0f, shootDelay);
		bc = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		myTransformX = this.transform.position.x;
		targetTransformX = targetTrans.position.x;

		if (!gameOver)
		{
			if (myTransformX > targetTransformX)
			{
				transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 0f);
			}
			else if (myTransformX < targetTransformX)
			{
				transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, 0f);
			}
			else
			{
				transform.position = new Vector3(targetTrans.position.x, transform.position.y, 0f);
			}
		}
	}

	void ShootRandom()
	{
		willShoot = Random.Range(0, 5);
		if (willShoot == 2)
		{
			Shoot();
		}
	}

	void Shoot()
	{
		if (!gameOver)
		{
			if (!laserActive)
			{
				bulletSpaceInvaders bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
				bullet.destroyed += BulletDestroyed;
				laserActive = true;
			}
		}
	}

	public void BulletDestroyed()
	{
		PickNewTarget();
		laserActive = false;
	}

	public void GameOver()
	{
		Destroy(this.gameObject);
	}

	void PickNewTarget()
	{
		int randomChildIdx = Random.Range(0, targetParent.transform.childCount);
		targetTrans = targetParent.transform.GetChild(randomChildIdx);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameOver == false)
		{
			if (other.gameObject.tag == "Missile")
			{
				anim.Play("PlayerHit");
				gameOver = true;
				Invoke("GameOver", gameOverDelay);
				gameManager.Invoke("Win", gameOverDelay);
			}
		}
	}
}

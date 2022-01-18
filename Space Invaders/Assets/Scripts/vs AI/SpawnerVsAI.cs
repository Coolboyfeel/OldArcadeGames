using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerVsAI : MonoBehaviour
{
	public float offsetMover;

	public InvaderVsAI[] prefabs;

	public MissleSpaceInvaders misslePrefab;
	public GameManagerVsAI gameManager;

	public int rows = 5;
	public int columns = 11;
	public AnimationCurve speed;
	public int spacing;
	private Vector3 direction;

	public int invadersKilled;
	public int invadersAlive => totalInvaders - invadersKilled;
	public int invadersTotal;
	public int totalInvaders => this.rows * this.columns;
	public float percentKilled => (float)this.invadersKilled / totalInvaders;

	public float missleAttackRate;
	public bool canShoot = false;
	public bool canMove = true;
	public float moveDelay;

	public float advanceRowDelay;
	public float latestDirection;
	public float oldDirection;
	public float refreshDirection;
	public float push;

	void Awake()
	{
		for (int row = 0; row < this.rows; row++)
		{
			float width = spacing * (columns - 1);
			float height = spacing * (rows - 1);
			Vector2 centering = new Vector2(-width / 2, -height / 2);
			Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spacing), 0.0f);

			for (int col = 0; col < this.columns; col++)
			{
				InvaderVsAI invader = Instantiate(this.prefabs[row], this.transform);
				Vector3 position = rowPosition;
				position.x += col * spacing;
				invader.transform.localPosition = position;
			}
		}
	}

	void Start()
	{
		InvokeRepeating("RefreshDirection", 0f, refreshDirection);
		Invoke("CanShootOn", missleAttackRate);
		invadersTotal = invadersAlive;
	}

	void Update()
	{
		if (invadersAlive == 0)
		{
			GameOver();
		}
		if (canMove)
		{
			latestDirection = Input.GetAxis("Horizontal");
		}
		direction = new Vector2(oldDirection, 0f);
		transform.position += direction * this.speed.Evaluate(percentKilled) * Time.deltaTime;

		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

		foreach (Transform invaderSpaceInvaders in this.transform)
		{
			if (!invaderSpaceInvaders.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (this.direction == Vector3.right && invaderSpaceInvaders.position.x >= (rightEdge.x - 1.0f))
			{
				canMove = false;
				Invoke("CanMoveOn", moveDelay);
				AdvanceRow();
				oldDirection = -1f;
				transform.position = new Vector3(transform.position.x - push, transform.position.y, 0f);
			}
			else if (this.direction == Vector3.left && invaderSpaceInvaders.position.x <= (leftEdge.x + 1.0f))
			{
				canMove = false;
				Invoke("CanMoveOn", moveDelay);
				AdvanceRow();
				oldDirection = 1f;
				transform.position = new Vector3(transform.position.x + push, transform.position.y, 0f);
			}
		}

		if (Input.GetAxis("Fire1") > 0)
		{
			if (canShoot)
			{
				MissleAttack();
			}
		}
	}

	void CanMoveOn()
	{
		canMove = true;
	}
	void CanShootOn()
	{
		canShoot = true;
	}

	void AdvanceRow()
	{
		Vector3 position = transform.position;
		position.y -= 1f;
		transform.position = position;
	}

	void MissleAttack()
	{
		canShoot = false;
		Invoke("CanShootOn", missleAttackRate);
		foreach (Transform invaderSpaceInvaders in this.transform)
		{
			if (!invaderSpaceInvaders.gameObject.activeInHierarchy)
			{
				continue;
			}
			if (Random.value < (1.0f / (float)invadersAlive))
			{
				Instantiate(misslePrefab, invaderSpaceInvaders.position,
					Quaternion.identity);
				break;
			}
		}
	}

	public void InvaderKilled()
	{
		invadersKilled++;
	}

	public void GameOver()
	{
		gameManager.GameOver();
		Destroy(this.gameObject);
	}

	public void Win()
	{
		gameManager.Win();
		Destroy(this.gameObject);
	}

	void RefreshDirection()
	{
		if (latestDirection > 0f)
		{
			oldDirection = 1f;
		}
		else if (latestDirection < 0f)
		{
			oldDirection = -1f;
		}
	}
}

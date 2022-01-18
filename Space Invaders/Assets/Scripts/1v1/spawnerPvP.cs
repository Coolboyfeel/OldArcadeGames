using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerPvP : MonoBehaviour
{
	public float offsetMover;

	public invaderPvP[] prefabs;

	public MissleSpaceInvaders misslePrefab;
	public GameManagerPvP gameManager;

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
				invaderPvP invader = Instantiate(this.prefabs[row], this.transform);
				Vector3 position = rowPosition;
				position.x += col * spacing;
				invader.transform.localPosition = position;
			}
		}
	}

	void Start()
	{
		Invoke("CanShootOn", missleAttackRate);
		invadersTotal = invadersAlive;
	}

	void Update()
	{
		if (invadersAlive == 0)
		{
			Win();
		}
		if (canMove)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				direction = new Vector2(-1f, 0f);
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				direction = new Vector2(1f, 0f);
			}
		}
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
			}
			else if (this.direction == Vector3.left && invaderSpaceInvaders.position.x <= (leftEdge.x + 1.0f))
			{
				canMove = false;
				Invoke("CanMoveOn", moveDelay);
				AdvanceRow();
			}
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(canShoot)
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
		direction.x *= -1f;

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
}
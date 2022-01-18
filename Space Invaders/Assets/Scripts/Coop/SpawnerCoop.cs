using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCoop : MonoBehaviour
{
	public float offsetMover;

	public InvaderCoop[] prefabs;

	public MissleSpaceInvaders misslePrefab;
	public GameManagerCoop gameManager;

	public int rows = 5;
	public int columns = 11;
	public AnimationCurve speed;
	public int spacing;
	private Vector3 direction = Vector2.right;

	public int invadersKilled;
	public int invadersAlive => totalInvaders - invadersKilled;
	public int invadersTotal;
	public int totalInvaders => this.rows * this.columns;
	public float percentKilled => (float)this.invadersKilled / totalInvaders;

	public float missleAttackRate;

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
				InvaderCoop invader = Instantiate(this.prefabs[row], this.transform);
				Vector3 position = rowPosition;
				position.x += col * spacing;
				invader.transform.localPosition = position;
			}
		}
	}

	void Start()
	{
		invadersTotal = invadersAlive;
		InvokeRepeating("MissleAttack", missleAttackRate, missleAttackRate);
	}

	void Update()
	{
		if (invadersAlive == 0)
		{
			Win();
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
				AdvanceRow();
			}
			else if (this.direction == Vector3.left && invaderSpaceInvaders.position.x <= (leftEdge.x + 1.0f))
			{
				AdvanceRow();
			}
		}
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "GameOverTrigger")
		{
			GameOver();
		}

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

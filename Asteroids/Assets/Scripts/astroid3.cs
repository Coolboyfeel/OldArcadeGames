using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid3 : MonoBehaviour
{
	public float speed;
	public float splitForce;
	public float maxLifetime;

	public float size = 1f;
	public float minSize = 0.5f;
	public float maxSize = 1.5f;

	public Sprite[] sprites;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private BoxCollider2D bc;
	public bool gameOver;
	private GameObject gameManager;

	void Awake()
	{
		bc = GetComponent<BoxCollider2D>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		sr.sprite = sprites[Random.Range(0, sprites.Length)];

		transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360.0f);
		transform.localScale = Vector3.one * size;

		rb.mass = size;
	}

	void Update()
	{

		gameManager = GameObject.FindGameObjectWithTag("GameManager");
		gameOver = gameManager.GetComponent<GameManagerAsteroidsCoop>().gameOver;
		if (gameOver == true)
		{
			GameOver();
		}
	}

	public void SetTrajectory(Vector2 direction)
	{
		rb.AddForce(direction * speed);

		Destroy(gameObject, maxLifetime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			if (size * 0.5f > minSize || size * 0.5f == minSize)
			{
				CreateSplit();
				CreateSplit();
			}

			FindObjectOfType<GameManagerAsteroidsCoop>().AsteroidDestroyedClassic(this);
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			FindObjectOfType<GameManagerAsteroidsCoop>().PlayerDiedClassic(this);
			Destroy(this.gameObject);
		}
		if (other.gameObject.tag == "Player2")
		{
			FindObjectOfType<GameManagerAsteroidsCoop>().Player2DiedClassic(this);
			Destroy(this.gameObject);
		}
	}

	void CreateSplit()
	{
		Vector2 position = transform.position;
		position += Random.insideUnitCircle * 0.5f;
		astroid3 half = Instantiate(this, position, transform.rotation);
		half.size = this.size * 0.5f;
		half.SetTrajectory(Random.insideUnitCircle.normalized * splitForce);
	}

	public void GameOver()
	{
		rb.gravityScale = 1;
		bc.isTrigger = true;
	}
}

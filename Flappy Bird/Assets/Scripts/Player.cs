using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private SpriteRenderer sr;
	public Sprite[] sprites;
    private int spriteIndex;
	public float spriteDelay = 0.15f;

	private Vector3 direction;
    public float gravity = -9.8f;
	public float jumpstrenght;

	public GameManager gamemanager;

	void Awake ()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Start ()
	{
		InvokeRepeating("AnimateSprite", spriteDelay, spriteDelay);
	}

	void OnEnable()
	{
		Vector3 position = transform.position;
		position.y = 0f;
		transform.position = position;
		direction = Vector3.zero;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			direction = Vector3.up * jumpstrenght;
		}

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				direction = Vector3.up * jumpstrenght;
			}
		}
		direction.y += gravity * Time.deltaTime;
		transform.position += direction * Time.deltaTime;
	}

	void AnimateSprite()
	{
		spriteIndex++;

		if (spriteIndex >= sprites.Length)
		{
			spriteIndex = 0;
		}

		sr.sprite = sprites[spriteIndex];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			gamemanager.GameOver();
		}
		else if (other.gameObject.tag == "Scoring")
		{
			gamemanager.IncreaseScore();
		}
	}
}

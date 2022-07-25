using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
	public float speed = 8f;
	public float multiplier = 1f;

	public Vector2 initialDirection;
	public LayerMask obstacleLayer;
	public Rigidbody2D rb { get; private set; }

	public Vector2 direction { get; private set; }
	public Vector2 nextDirection { get; private set; }
	public Vector3 startingPos;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		startingPos = transform.position;
	}

	void Start()
	{
		ResetState();
	}

	public void ResetState()
	{
		multiplier = 1.0f;
		direction = initialDirection;
		nextDirection = Vector2.zero;
		transform.position = startingPos;
		rb.isKinematic = false;
		enabled = true;
	}

	void Update()
	{
		if (nextDirection != Vector2.zero)
		{
			SetDirection(nextDirection);
		}
	}

	void FixedUpdate()
	{
		Vector2 position = rb.position;
		Vector2 translation = direction * speed * multiplier * Time.fixedDeltaTime;
		rb.MovePosition(position + translation);
	}

	public void SetDirection(Vector2 direction, bool forced = false)
	{
		if(forced || !Occupied(direction))
		{
			this.direction = direction;
			nextDirection = Vector2.zero;
		}
		else
		{
			nextDirection = direction;
		}
	}

	public bool Occupied(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 
			0.75f, 0.0f, direction, 1.5f, obstacleLayer);
		return hit.collider != null;
	}
}

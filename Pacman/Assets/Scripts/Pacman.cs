using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
	public Movement movement { get; private set; }

	void Awake()
	{
		movement = GetComponent<Movement>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			movement.SetDirection(Vector2.up);
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			movement.SetDirection(Vector2.down);
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			movement.SetDirection(Vector2.left);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			movement.SetDirection(Vector2.right);
		}

		float angle = Mathf.Atan2(movement.dir.y, movement.dir.x);
		transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
	}
}
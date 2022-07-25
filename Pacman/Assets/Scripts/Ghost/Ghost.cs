using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	public Rigidbody2D rb {get; private set;}
	public Movement movement {get; private set; } 
	public GhostHome home {get; private set; }
	public GhostScatter scatter {get; private set; }
	public GhostChase chase {get; private set; }
	public GhostFrightened frightened {get; private set; }
	public GhostBehavoir initialBehavior;
	public Transform target;

	public int points = 200;

	void Awake() 
	{
		movement = GetComponent<Movement>();
		home = GetComponent<GhostHome>();
		scatter = GetComponent<GhostScatter>();
		chase = GetComponent<GhostChase>();
		frightened = GetComponent<GhostFrightened>();
	}

	private void Start() 
	{
		ResetState();
	}

	public void ResetState() 
	{
		gameObject.SetActive(true);
		movement.ResetState();
		
		frightened.Disable();
		chase.Disable();
		scatter.Enable();
		
		if(home != initialBehavior) 
		{
			home.Disable();
		}

		if (initialBehavior != null) 
		{
			initialBehavior.Enable();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) 
		{
			if(frightened.enabled) 
			{
				FindObjectOfType<GameManager>().GhostEaten(this);
			} else {
				FindObjectOfType<GameManager>().PacmanEaten();
				this.chase.enabled = false;
				this.scatter.enabled = true;
			}
		}
	}
}

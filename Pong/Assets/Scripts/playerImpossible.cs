using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerImpossible : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    public float speed;
    public bool gameOver;
    public GameObject GameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        gameOver = GameManager.GetComponent<ScoringImpossible>().gameOver;
		direction = new Vector2(0f, Input.GetAxis("Vertical"));
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

    public void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}

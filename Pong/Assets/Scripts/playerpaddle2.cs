using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpaddle2: MonoBehaviour
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
        gameOver = GameManager.GetComponent<Scoring1V1>().gameOver;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }
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

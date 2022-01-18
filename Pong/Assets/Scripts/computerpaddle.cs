using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class computerpaddle : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D ballRb;
    public Transform ballTrans;
    public float speed;
    public float trackX;
    public bool gameOver;
    public GameObject GameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (gameOver == false) 
        {
            if (this.transform.position.x < 0f)
            {
                if (this.ballTrans.position.x < trackX)
                {
                    if (this.ballRb.position.y > this.transform.position.y)
                    {
                        rb.AddForce(Vector2.up * this.speed);
                    }
                    else if (this.ballRb.position.y < this.transform.position.y)
                    {
                        rb.AddForce(Vector2.down * this.speed);
                    }
                }
                else
                {
                    if (this.transform.position.y > 0.0f)
                    {
                        rb.AddForce(Vector2.down * this.speed);
                    }
                    else if (this.transform.position.y < 0.0f)
                    {
                        rb.AddForce(Vector2.up * this.speed);
                    }
                }
            }
            if (this.transform.position.x > 0f)
            {
                if (this.ballTrans.position.x > trackX)
                {
                    if (this.ballRb.position.y > this.transform.position.y)
                    {
                        rb.AddForce(Vector2.up * this.speed);
                    }
                    else if (this.ballRb.position.y < this.transform.position.y)
                    {
                        rb.AddForce(Vector2.down * this.speed);
                    }
                }
                else
                {
                    if (this.transform.position.y > 0.0f)
                    {
                        rb.AddForce(Vector2.down * this.speed);
                    }
                    else if (this.transform.position.y < 0.0f)
                    {
                        rb.AddForce(Vector2.up * this.speed);
                    }
                }
            }
        }      
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void Update()
    {
        gameOver = GameManager.GetComponent<Scoring>().gameOver;
    }
}

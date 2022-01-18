using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float bounceStrength;
    public GameObject GameManager;
    public bool gameOver;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetPosition();
    }


    void Update() 
    {
        gameOver = GameManager.GetComponent<Scoring>().gameOver; 
        if (gameOver == true) 
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void AddStartingForce()
    {
        if (gameOver == false) 
        {
            float x = Random.value < 0.5f ? -1f : 1f;
            float y = Random.value < 0.5f ? Random.Range(-1f, -0.5f) :
                                            Random.Range(0.5f, 1f);

            Vector2 direction = new Vector2(x, y);
            rb.AddForce(direction * this.speed);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Vector2 normal = other.GetContact(0).normal;
            rb.AddForce(normal * this.bounceStrength);
        }
        else if (other.gameObject.tag == "Computer")
        {
            Vector2 normal = other.GetContact(0).normal;
            rb.AddForce(normal * this.bounceStrength);
        }
    }

    public void ResetPosition() 
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;

        AddStartingForce();
    }
}

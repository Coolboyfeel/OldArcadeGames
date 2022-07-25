using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float multiplier;
    public Rigidbody2D rb {get; private set;}
    public float speed = 500f;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Invoke(("SetRandomTrajectory"), 1f);
    }

    public void ResetBall() 
    {
        transform.position = new Vector2(0, -3);
        rb.velocity = Vector2.zero;
        Invoke(("SetRandomTrajectory"), 1f); 
    }

    private void SetRandomTrajectory() 
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y= -1f;

        rb.AddForce(force * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(gameObject.tag == "Paddle") 
        {
            rb.velocity = multiplie r * rb.velocity.magnitude;
        }
    }
}

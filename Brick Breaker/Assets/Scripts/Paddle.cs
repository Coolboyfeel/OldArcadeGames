using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public GameManager gameManager;
    public Rigidbody2D rb {get; private set;} 
    public float speed = 30f;
    public Vector2 direction {get; private set; }
    public float maxBounceAngle = 75f;

    private void Awake() 
    {
        this.rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ResetPaddle() 
    {
        transform.position = new Vector2(0f, transform.position.y);
        rb.velocity = Vector2.zero;
    }

    private void Update() {
        if (Input.GetKey(gameManager.activeKey[0]))
        {
            this.direction = Vector2.left;
        } else if (Input.GetKey(gameManager.activeKey[1])) {
            this.direction = Vector2.right;
        } else {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if(this.direction != Vector2.zero) {
            this.rb.AddForce(this.direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball != null) 
        {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = other.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = other.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
        }
    }
}

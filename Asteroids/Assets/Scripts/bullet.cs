using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float LifeTime;

    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        Project(this.transform.up);
    }
    
    public void Project(Vector2 direction) 
    {
        rb.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.LifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Boundary" || 
			other.gameObject.tag == "Asteroid" ||
			other.gameObject.tag == "Asteroid2")
		{
			Destroy(this.gameObject);
		}
    }
}

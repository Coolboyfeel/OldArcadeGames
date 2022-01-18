using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpaceInvaders : MonoBehaviour
{
    public Vector3 direction;

	public int lifeTime = 3;
	public float speed;
    public System.Action destroyed;
	

	void Awake() 
	{
		Invoke("Die", lifeTime);
	}

	void Die() 
	{
		destroyed.Invoke();
		Destroy(this.gameObject);
	}

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag == "Boundary")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Invader")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Bunker")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
	}
    void OnCollisionEnter2D(Collision2D other) 
    {
		if (other.gameObject.tag == "Boundary")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Invader")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Bunker")
		{
			destroyed.Invoke();
			Destroy(this.gameObject);
		}
	}
}

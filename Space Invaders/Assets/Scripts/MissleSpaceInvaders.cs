using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpaceInvaders : MonoBehaviour
{
	public Vector3 direction;

	public float speed;

	void Awake()
	{
		transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
	}
	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
		else if(other.gameObject.tag == "Boundary")
		{
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Bunker")
		{
			Destroy(this.gameObject);
		}
	}
}

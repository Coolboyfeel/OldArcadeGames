using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunkerSpaceInvaders : MonoBehaviour
{
	public float lives;
	public Sprite normal;
	public Sprite hit1;
	public Sprite hit2;
	private SpriteRenderer sr;

	void Awake() 
	{
		sr = GetComponent<SpriteRenderer>();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Invader")
		{
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Missile")
		{
			if (lives > 0f)
			{
				lives--;
			}
		}
	}

	void Update()
	{
        if (lives <= 0f)
        {
            Destroy(this.gameObject);
        }
        else if (lives == 3f)
        {
            sr.sprite = normal;
        }
        else if (lives == 2f)
        {
            sr.sprite = hit1;
        }
		else if (lives == 1f) 
		{
			sr.sprite = hit2;
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidsflying : MonoBehaviour
{
	public ParticleSystem explosionBig;
	public ParticleSystem explosionSmall;
	public ParticleSystem explosionMini;
	public float speed;
	public float splitForce;
	public float maxLifetime;
	public bool CanDestroy;
	public float ignoreDelay;

	public float size = 1f;
	public float minSize = 0.5f;
	public float maxSize = 1.5f;

	public Sprite[] sprites;
	private SpriteRenderer sr;
	private BoxCollider2D bc;
	private Rigidbody2D rb;

	void Awake()
	{
		explosionBig = GameObject.FindGameObjectWithTag("Explosion1").
			GetComponent<ParticleSystem>();
		explosionSmall = GameObject.FindGameObjectWithTag("Explosion2").
			GetComponent<ParticleSystem>();
		explosionMini = GameObject.FindGameObjectWithTag("Explosion3").
			GetComponent<ParticleSystem>();
		bc = GetComponent<BoxCollider2D>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		sr.sprite = sprites[Random.Range(0, sprites.Length)];

		transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360.0f);
		transform.localScale = Vector3.one * size;

		rb.mass = size;
	}

	public void SetTrajectory(Vector2 direction)
	{
		rb.AddForce(direction * speed);

		Destroy(gameObject, maxLifetime);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (CanDestroy == true)
		{
			if (other.gameObject.tag == "Asteroid2")
			{
				if (size * 0.5f > minSize || size * 0.5f == minSize)
				{
					CreateSplit();
					CreateSplit();
				}
				CanDestroy = false;
				Destroy(gameObject);
				if (size > 1.09f)
				{
					this.explosionBig.transform.position = transform.position;
					this.explosionBig.Play();
				}
				else if (size > 0.75)
				{
					this.explosionSmall.transform.position = transform.position;
					this.explosionSmall.Play();
				}
				else
				{
					this.explosionMini.transform.position = transform.position;
					this.explosionMini.Play();
				}
				Invoke("Ignore", ignoreDelay);
			}
		}
	}

	void Ignore()
	{
		CanDestroy = true;
	}

	void CreateSplit()
	{
		Vector2 position = transform.position;
		position += Random.insideUnitCircle * 0.5f;
		astroidsflying half = Instantiate(this, position, transform.rotation);
		half.size = this.size * 0.5f;
		half.SetTrajectory(Random.insideUnitCircle.normalized * splitForce);
	}
}

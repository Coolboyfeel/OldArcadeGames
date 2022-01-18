using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderCoop : MonoBehaviour
{

    public Sprite[] animationSprites;
    public float animationTime;
    public GameObject ps;
    private SpriteRenderer sr;
    private int animationFrame;
    private SpawnerCoop spawner;
    private GameManagerCoop gameManager;
    public float destroyDelay;
	private bool gameOver;


	void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerCoop>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerCoop>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating("AnimateSprite", animationTime, animationTime);
    }

    void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame > this.animationSprites.Length ||
            animationFrame == this.animationSprites.Length)
        {
            animationFrame = 0;
        }
        sr.sprite = animationSprites[animationFrame];

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            sr.enabled = false;
            Instantiate(ps, transform.position,
                Quaternion.identity, transform);
            Destroy(gameObject, destroyDelay);
            spawner.InvaderKilled();
        }

		if (other.gameObject.tag == "GameOverTrigger")
		{
			if (!gameOver)
			{
				gameOver = true;
				spawner.GameOver();
				Destroy(this.gameObject);
			}
		}

	}

    public void GameOver()
    {
        Destroy(this.gameObject);
    }
}

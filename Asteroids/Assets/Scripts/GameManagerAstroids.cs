using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAstroids : MonoBehaviour
{
    public ParticleSystem explosionBig;
    public ParticleSystem explosionSmall;
    public ParticleSystem explosionMini;
    public float score;
	public float respawnTime;
	public float lives;
	public Text livesText;
	public Text lives2Text;
    public Text scoreText;
    public Text finalScoreText;
	public float collisionDelay;
	public Animator animCanvas;
	public bool gameOver;

	public player player;

    public void AsteroidDestroyed(astroid Astroids) 
    {
        if (Astroids.size > 1.09f) 
        {
            this.explosionBig.transform.position = Astroids.transform.position;
            this.explosionBig.Play();
            if (Astroids.size > 1.3f) 
            {
                score += 750f;
            }
            else 
            {
                score += 500f;
            }
        }
        else if (Astroids.size > 0.75f) 
        {
            this.explosionSmall.transform.position = Astroids.transform.position;
            this.explosionSmall.Play();
            if (Astroids.size > 0.65f) 
            {
                score += 400f;
            }
            else 
            {
                score += 200f;
            }
        }
        else 
        {
            this.explosionMini.transform.position = Astroids.transform.position;
            this.explosionMini.Play();
            if (Astroids.size > 0.6f)
            {
                score += 100f;
            }
            else 
            {
                score += 50f;
            }
        }

    }

    public void AsteroidDestroyedClassic(astroid2 Astroids2)
	{
		if (Astroids2.size > 1.09f)
		{
			this.explosionBig.transform.position = Astroids2.transform.position;
			this.explosionBig.Play();
			if (Astroids2.size > 1.3f)
			{
				score += 50f;
			}
			else
			{
				score += 100f;
			}
		}
		else if (Astroids2.size > 0.75f)
		{
			this.explosionSmall.transform.position = Astroids2.transform.position;
			this.explosionSmall.Play();
			if (Astroids2.size > 0.65f)
			{
				score += 200f;
			}
			else
			{
				score += 400f;
			}
		}
		else
		{
			this.explosionMini.transform.position = Astroids2.transform.position;
			this.explosionMini.Play();
			if (Astroids2.size > 0.6f)
			{
				score += 500f;
			}
			else
			{
				score += 750f;
			}
		}

	}

	public void PlayerHit(astroid Astroids)  
    {
		if (Astroids.size > 1.2)
		{
			lives = lives - 2f;
		}
		else
		{
			lives = lives - 1f;
		}
		if (lives < 0 || lives == 0)
		{
			GameOver();
		}
		if (Astroids.size > 1.09f)
        {
            this.explosionBig.transform.position = Astroids.transform.position;
            this.explosionBig.Play();
        }
        else if (Astroids.size > 0.75)
        {
            this.explosionSmall.transform.position = Astroids.transform.position;
            this.explosionSmall.Play();
        }
        else
        {
            this.explosionMini.transform.position = Astroids.transform.position;
            this.explosionMini.Play();
        }
    }

	public void PlayerDiedClassic(astroid2 Astroids2)
	{
		if (Astroids2.size > 1.09f)
		{
			lives--;
			lives--;
		}
	    else
		{
			lives--;
		}
		if (Astroids2.size > 1.09f)
		{
			this.explosionBig.transform.position = Astroids2.transform.position;
			this.explosionBig.Play();
		}
		else if (Astroids2.size > 0.75)
		{
			this.explosionSmall.transform.position = Astroids2.transform.position;
			this.explosionSmall.Play();
		}
		else
		{
			this.explosionMini.transform.position = Astroids2.transform.position;
			this.explosionMini.Play();
		}

		if (lives < 0 || lives == 0)
		{
			player.GameOver();
		}
		else
		{
			player.gameObject.SetActive(false);
			Invoke("Respawn", respawnTime);
		}
		if (lives == 0 || lives < 0)
		{
			GameOver();
		}
	}
	

	public void Respawn()
	{
		player.transform.position = Vector3.zero;
		player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
		player.gameObject.SetActive(true);
		Invoke("TurnOnCollision", collisionDelay);
	}


	void TurnOnCollision()
	{
		player.gameObject.layer = LayerMask.NameToLayer("Player");
	}

    void Update() 
    {
		if (lives < 0)
		{
			lives = 0f;
		}

		livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();
    }

	void GameOver()
	{
		gameOver = true;
		player.GameOver();
		animCanvas.Play("gameOverCanvas");
	}
}

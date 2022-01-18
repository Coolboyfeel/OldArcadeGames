using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAsteroidsCoop : MonoBehaviour
{
	public ParticleSystem explosionBig;
	public ParticleSystem explosionSmall;
	public ParticleSystem explosionMini;
	public float score;
	public float respawnTime;
	public float lives;
	public float lives2;
	public Text livesText;
	public Text lives2Text;
	public Text scoreText;
	public Text finalScoreText;
	public float collisionDelay;
	public Animator animCanvas;
	public bool gameOver;

	public player player;
	public playerCoop player2;
	public astroid3 asteroid3;

	public void AsteroidDestroyedClassic(astroid3 Astroids3)
	{
		if (Astroids3.size > 1.09f)
		{
			this.explosionBig.transform.position = Astroids3.transform.position;
			this.explosionBig.Play();
			if (Astroids3.size > 1.3f)
			{
				score += 50f;
			}
			else
			{
				score += 100f;
			}
		}
		else if (Astroids3.size > 0.75f)
		{
			this.explosionSmall.transform.position = Astroids3.transform.position;
			this.explosionSmall.Play();
			if (Astroids3.size > 0.65f)
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
			this.explosionMini.transform.position = Astroids3.transform.position;
			this.explosionMini.Play();
			if (Astroids3.size > 0.6f)
			{
				score += 500f;
			}
			else
			{
				score += 750f;
			}
		}

	}



	public void PlayerDiedClassic(astroid3 Astroids3)
	{
		if (Astroids3.size > 1.09f)
		{
			lives--;
			lives--;
		}
		else
		{
			lives--;
		}
		if (Astroids3.size > 1.09f)
		{
			this.explosionBig.transform.position = Astroids3.transform.position;
			this.explosionBig.Play();
		}
		else if (Astroids3.size > 0.75)
		{
			this.explosionSmall.transform.position = Astroids3.transform.position;
			this.explosionSmall.Play();
		}
		else
		{
			this.explosionMini.transform.position = Astroids3.transform.position;
			this.explosionMini.Play();
		}

		if (lives < 0 && lives2 < 0 || lives == 0 && lives2 == 0)
		{
			GameOver();
		}
		else if (lives < 0 || lives == 0)
		{
			player.GameOver();
		}
		else
		{
			player.gameObject.SetActive(false);
			Invoke("Respawn", respawnTime);
		}
	}

	public void Player2DiedClassic(astroid3 Astroids3)
	{
		if (Astroids3.size > 1.09f)
		{
			lives2--;
			lives2--;
		}
		else
		{
			lives2--;
		}
		if (Astroids3.size > 1.09f)
		{
			this.explosionBig.transform.position = Astroids3.transform.position;
			this.explosionBig.Play();
		}
		else if (Astroids3.size > 0.75)
		{
			this.explosionSmall.transform.position = Astroids3.transform.position;
			this.explosionSmall.Play();
		}
		else
		{
			this.explosionMini.transform.position = Astroids3.transform.position;
			this.explosionMini.Play();
		}
		if (lives < 0 && lives2 < 0 || lives == 0 && lives2 == 0)
		{
			GameOver();
		}
		else if (lives2 < 0 || lives2 == 0)
		{
			player2.GameOver();
		}
		else
		{
			player2.gameObject.SetActive(false);
			Invoke("Respawn2", respawnTime);
		}
		
	}


	public void Respawn()
	{
		player.transform.position = Vector3.zero;
		player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
		player.gameObject.SetActive(true);
		Invoke("TurnOnCollision", collisionDelay);
	}

	public void Respawn2()
	{
		player2.transform.position = Vector3.zero;
		player2.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
		player2.gameObject.SetActive(true);
		Invoke("TurnOnCollision2", collisionDelay);
	}

	void TurnOnCollision()
	{
		player.gameObject.layer = LayerMask.NameToLayer("Player");
	}

	void TurnOnCollision2()
	{
		player2.gameObject.layer = LayerMask.NameToLayer("Player");
	}

	void Update()
	{
		if (lives < 0)
		{
			lives = 0f;
		}
		if (lives2 < 0)
		{
			lives2 = 0f;
		}

		livesText.text = lives.ToString();
		lives2Text.text = lives2.ToString();
		scoreText.text = score.ToString();
		finalScoreText.text = score.ToString();
	}

	void GameOver()
	{
		gameOver = true;
		player.GameOver();
		player2.GameOver();
		animCanvas.Play("gameOverCanvas");
	}
}

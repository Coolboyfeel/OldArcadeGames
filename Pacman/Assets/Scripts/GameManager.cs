using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Ghost[] ghosts;
	public Image[] livesIcon;

	public Pacman pacman;
	

	public Transform pellets;

	public int ghostMultiplier { get; private set; } = 1;

	public int score;
	public int lives;

	void Start()
	{
		NewGame();
	}

	void Update()
	{
		if(this.lives <= 0 && Input.anyKeyDown) {
			NewGame();
		}
	}
	
	void NewGame()
	{
		SetScore(0);
		SetLives(3);
		NewRound();
	}

	void NewRound()
	{
		foreach (Transform pellet in this.pellets)
		{
			pellet.gameObject.SetActive(true);
		}

		ResetState();
	}

	void ResetState()
	{
		ResetMultiplier();
		for (int i = 0; i < this.ghosts.Length; i++)
		{
			this.ghosts[i].ResetState();
		}

		this.pacman.ResetState();
	}

	void GameOver()
	{
		for (int i = 0; i < this.ghosts.Length; i++)
		{
			this.ghosts[i].gameObject.SetActive(false);
		}

		this.pacman.gameObject.SetActive(false);
	}

	void SetScore(int score)
	{
		this.score = score;
	}

	void SetLives(int lives)
	{
		this.lives = lives;
	}

	public void GhostEaten(Ghost ghost)
	{
		int points = ghost.points * ghostMultiplier;
		SetScore(this.score + ghost.points);
		ghostMultiplier++;

	}

	public void PacmanEaten()
	{
		this.pacman.gameObject.SetActive(false);

		SetLives(this.lives - 1);

		for (int i = 0; i < this.livesIcon.Length; i++)
		{
			int livesNum = i + 1;

			if (lives == livesNum) 
			{
				this.livesIcon[i].enabled = true;
			}
			else if (lives > livesNum) 
			{
				this.livesIcon[i].enabled = true;
			}
			else 
			{
				this.livesIcon[i].enabled = false;
			}
		}
		
		if(this.lives > 0)
		{
			Invoke("ResetState", 3.0f);
		}
		else
		{
			GameOver();
		}

	}

	public void PelletEaten(Pellet pellet)
	{
		pellet.gameObject.SetActive(false);

		SetScore(this.score + pellet.points);

		if (!HasRemainingPellets())
		{
			pacman.gameObject.SetActive(false);
			Invoke("NewRound", 3.0f);
		}
	}

	public void PowerPelletEaten(PowerPellet pellet)
	{
		for(int i = 0; i < this.ghosts.Length; i++) {
			this.ghosts[i].frightened.Enable(pellet.duration);
		}

		PelletEaten(pellet);
		CancelInvoke();
		Invoke("ResetMultiplier", pellet.duration);	
	}

	bool HasRemainingPellets()
	{
		foreach (Transform pellet in this.pellets)
		{
			if(pellet.gameObject.activeSelf)
			{
				return true;
			}
		}

		return false;
	}

	void ResetMultiplier()
	{
		ghostMultiplier = 1;
	}
}

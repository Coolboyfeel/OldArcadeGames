using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Text scoreText;
	public GameObject playButton;
	public GameObject gameover;
	public Player player;
	public int score;

	void Awake()
	{
		Pause();
	}

	public void Play()
	{
		score = 0;
		scoreText.text = score.ToString();

		playButton.SetActive(false);
		gameover.SetActive(false);

		player.enabled = true;
		Time.timeScale = 1f;

		Pipes[] pipes = FindObjectsOfType<Pipes>();
		for (int i = 0; i < pipes.Length; i++)
		{
			Destroy(pipes[i].gameObject);
		}
	}

	public void Pause()
	{
		player.enabled = false;
		Time.timeScale = 0f;
	}

	public void GameOver()
	{
		gameover.SetActive(true);
		playButton.SetActive(true);
		Debug.Log("GameOver");

		Pause();
	}

	public void IncreaseScore()
	{
		score++;
	}

	void Update()
	{
		scoreText.text = score.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidSpawnerFlying : MonoBehaviour
{
	public astroidsflying asteroidPrefab;
	public float trajectoryVariance = 15f;
	public float spawnRate = 2f;
	public float spawnDistance = 15f;
	public int spawnAmount = 1;

	void Start()
	{
		InvokeRepeating("Spawn", spawnRate, spawnRate);
	}

	void Spawn()
	{
		for (int i = 0; i < spawnAmount; i++)
		{
			Vector3 spawnDirection = Random.insideUnitCircle.
				normalized * spawnDistance;
			Vector3 spawnPoint = transform.position + spawnDirection;

			float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
			Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

			astroidsflying asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
			asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
			asteroid.SetTrajectory(rotation * -spawnDirection);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;

	public float spawnRate = 1f;

	public float minHeight = -1f;
	public float maxHeight = 1f;

	void OnEnable()
	{
		InvokeRepeating("Spawn", spawnRate, spawnRate);
	}

	void OnDisable()
	{
		CancelInvoke("Spawn");
	}

	void Spawn()
	{
		GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
		pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
	public ParticleSystem ps;

	void Awake()
	{
		ps.transform.position = new Vector3(transform.position.x,
			transform.position.y, transform.position.z);
		ps.Play();
	}
}

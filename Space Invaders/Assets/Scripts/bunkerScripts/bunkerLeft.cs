using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunkerLeft : MonoBehaviour
{
	public GameObject parentBunker;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Missile")
		{
			bunkerSpaceInvaders bunker = parentBunker.GetComponent<bunkerSpaceInvaders>();

		}
	}
}

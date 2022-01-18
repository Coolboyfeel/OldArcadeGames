using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class AnimatedSprite : MonoBehaviour
{
	public SpriteRenderer sr { get; private set; }

	public Sprite[] sprites;
	public float animationTime = 0.25f;

	public int animationFrame { get; private set; }
	public bool loop = true;

	void Awake()
	{
		this.sr = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		InvokeRepeating("Advance", animationTime, animationTime);
	}

	void Advance()
	{
		if(!sr.enabled)
		{
			return;
		}

		animationFrame++;

		if(animationFrame >= sprites.Length && loop)
		{
			animationFrame = 0;
		}

		if (animationFrame >= 0 && animationFrame < sprites.Length)
		{
			sr.sprite = sprites[animationFrame];
		}
	}

	void Restart()
	{
		animationFrame = -1;
		Advance();
	}
}

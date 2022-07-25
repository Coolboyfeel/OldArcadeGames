using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set;}
    public SpriteRenderer sr {get; private set;}
    public Sprite[] states;
    public int basePoints {get; private set;} = 100;
    public int points  {get; private set;}
    public bool unbreakable;

    public GameManager gameManager;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Start() {
        if(!this.unbreakable) 
        {
            health = states.Length;
            sr.sprite = states[health - 1];
            float originalHealth = health;
            points = basePoints * health;
        }
    }

    private void Hit() 
    {
        if (unbreakable) 
        {
            return;
        }
        health--;

        if(health <= 0) {
            gameObject.SetActive(false);
            gameManager.Hit(this);
        } else {
            sr.sprite = states[health - 1];   
        }

       
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ball") 
        {
            Hit();
        }
    }
}

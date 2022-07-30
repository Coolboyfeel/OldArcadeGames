using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set;}
    public SpriteRenderer sr {get; private set;}
    public Sprite[] states;
    public Texture2D[] statesTextures;
    public int points {get; private set;} = 100;
    public bool unbreakable;
    public ParticleSystem broken;
    public int powerUpChance;
    public int randomNum;
    public GameObject powerUp;
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
        }
    }

    public void Hit(string obj) 
    {
        if (unbreakable && obj != "Lazer") 
        {
            return;
        }

        int currentPoints = points;
        if(obj == "Lazer") 
        {
            currentPoints = 50;
        }
        
        
        health--;

        if(health <= 0) {
            StartCoroutine(Break());
            
            
        } else {
            sr.sprite = states[health - 1];
            
        }
        gameManager.Hit(this, currentPoints);
    }

    IEnumerator Break() 
    {
        broken.gameObject.transform.position = this.transform.position;
        broken.Play();
        
        randomNum = Random.Range(1, powerUpChance + 1);
        if(randomNum == 1) 
        {
            Instantiate(powerUp, this.transform.position, Quaternion.identity);
        }
        
        sr.sprite = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
          
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
        
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ball") 
        {
            Hit("Ball");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set;}
    public int startHealth;
    public SpriteRenderer sr {get; private set;}
    private BoxCollider2D bc;
    public Sprite[] states;
    public Texture2D[] statesTextures;
    public int points {get; private set;} = 100;
    public bool unbreakable;
    public ParticleSystem broken;
    public int powerUpChance;
    public int randomNum;
    public GameObject powerUp;
    public GameManager gameManager;
    private Animator anim;

    public float RewindWait;


    private void Awake() {
        
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Start() {

        if(!this.unbreakable) 
        {
            health = states.Length;
            startHealth = health;
            sr.sprite = states[health - 1];
        }
    }

    private void Update() {
        if(gameManager.rewindActive) 
        {
            bc.enabled = true;
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
            anim.Play("Hit");
            sr.sprite = states[health - 1];
            
        }
        gameManager.Hit(this, currentPoints);
    }

    IEnumerator Break() 
    {
        sr.enabled = false;
        bc.enabled = false;

        broken.gameObject.transform.position = this.transform.position;
        broken.Play();
        
        randomNum = Random.Range(1, powerUpChance + 1);
        if(randomNum == 1) 
        {
            Instantiate(powerUp, this.transform.position, Quaternion.identity);
        }
        
        sr.sprite = null;
        
          
        yield return new WaitForSeconds(RewindWait);
        if(sr.enabled == false) {
            this.gameObject.SetActive(false);
        }  
    }
        
    
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.tag == "Ball") 
        {
            Debug.Log("Collided with ball");
            if(!gameManager.rewindActive) 
            {
                Debug.Log("not rewinding");
                Hit("Ball");
            } else {
                if(health <= 0) {
                    Debug.Log("rewinding");
                    StopCoroutine(Break()); 
                    sr.enabled = true;
                    sr.sprite = states[0];
                    health = 1;
                } else if (health < startHealth) {
                    health++;
                }                   
            }      
        }
    }
}

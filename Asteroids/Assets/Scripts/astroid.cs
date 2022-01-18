using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid : MonoBehaviour
{
    //Viarables
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Transform target;
    private GameObject spawnPoint;
    private Animator anim;
    public float speed;

    public float size;
    public float lifes;
    public float minSize;
    public float maxSize;
    public float rotation;
    public string mode;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    private int sprite;
    public bool gameOver;

    //Getting the Components
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").
                GetComponent<Transform>();
        
            
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    //picks a random scale, rotation and sprite
    void Start() 
    {
        size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 0f);
        rb.mass = this.size;
        if (size > 1.09f)
        {
            if (size > 1.3f)
            {
                lifes = 3f;
            }
            else
            {
                lifes = 2f;
            }
        }

        sprite = Random.Range(1, 4);
        if (sprite == 1) 
        {
            sr.sprite = sprite1;
        }
        else if (sprite == 2)
        {
            sr.sprite = sprite2;
        }
        else if(sprite == 3)
        {
            sr.sprite = sprite3;
        }
        else if(sprite == 4)
        {
            sr.sprite = sprite4;
        }

        rotation = Random.Range(0, 360);
        transform.rotation = new Quaternion(0f, rotation, 0f, 0f);
        
  

    }

    void Update() 
    {
        gameOver = target.GetComponent<player>().gameOver;
        
        transform.position = Vector2.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);

        if (gameOver == true) 
        {
            Invoke("GameOver", 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            FindObjectOfType<GameManagerAstroids>().PlayerHit(this);
            Destroy(this.gameObject);
        }   
    }

    void GameOver()
    {
        rb.gravityScale = 1;
        bc.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (size > 1.09)
            {
                if (lifes > 1)
                {
                    anim.Play("hit");
                    lifes = lifes - 1;
                }
                else
                {
                    FindObjectOfType<GameManagerAstroids>().AsteroidDestroyed(this);
                    Destroy(this.gameObject);   
                }
            }
            else 
            {
                FindObjectOfType<GameManagerAstroids>().AsteroidDestroyed(this);
                Destroy(this.gameObject);
            }
        }
    }
}

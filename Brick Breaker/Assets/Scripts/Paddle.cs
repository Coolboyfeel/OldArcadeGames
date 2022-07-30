using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public GameManager gameManager;
    public Rigidbody2D rb {get; private set;} 
    public BoxCollider2D bc {get; private set;}
    public LineRenderer lr {get; private set;}
    public Vector2 direction {get; private set; }
    
    public float maxBounceAngle = 75f;
    public bool occupied = false;

    [Header("Lazer/RayCast")]
    public LayerMask layer;
    public float lazerDuration;
    private Vector2 lazerPos;
    public float lineDur;
    public float speed = 30f;
    public bool shooting = false;
    public int lazerAmmo;
    public float lazerOffset;

    private void Awake() 
    {
        layer = ~layer;
        this.rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        bc = GetComponent<BoxCollider2D>();
        lr = GetComponent<LineRenderer>();
    }

    public void ResetPaddle() 
    {
        shooting = false;
        lazerAmmo = 0;
        lr.positionCount = 0;
        transform.position = new Vector2(0f, transform.position.y);
        transform.localScale = new Vector3(1f, 1f, 1f);
        rb.velocity = Vector2.zero;
    }

    private void Update() {
        if(!gameManager.actives[3]) 
        {
            if (Input.GetKey(gameManager.activeKey[0]))
            {
                this.direction = Vector2.left;
            } else if (Input.GetKey(gameManager.activeKey[1])) {
                this.direction = Vector2.right;
            } else {
                this.direction = Vector2.zero;
            }
        } else if(gameManager.actives[3]) {
            if (Input.GetKey(gameManager.activeKey[1]))
            {
                this.direction = Vector2.left;
            } else if (Input.GetKey(gameManager.activeKey[0])) {
                this.direction = Vector2.right;
            } else {
                this.direction = Vector2.zero;
            }
        }
        
        
        
        //RaycastHit2D hit =  Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, 0f), transform.up, maxRayDistance, 2);
        

        if(Input.GetKeyDown(KeyCode.Q) && lazerAmmo > 0 && !shooting) 
        {
            lazerPos = transform.position;
            lazerAmmo--;
            StartCoroutine(ShootBool());    
        }

        if(shooting) 
        {
            var ray = new Ray2D(new Vector2(lazerPos.x, lazerPos.y + 1.5f), Vector2.up);
            RaycastHit2D hit = Physics2D.Raycast(origin: ray.origin, direction: ray.direction, distance: Mathf.Infinity, layerMask: layer);
            if(hit && hit.collider.gameObject.tag == "Brick") 
            {
                Debug.DrawRay(lazerPos, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
                hit.collider.gameObject.GetComponent<Brick>().Hit("Lazer");
                lr.SetPosition(0, new Vector3(lazerPos.x, lazerPos.y + lazerOffset, 0f));
                lr.SetPosition(1, hit.point);
            } else {
                Debug.DrawRay(lazerPos, transform.TransformDirection(Vector3.up) * Mathf.Infinity, Color.green);
                lr.SetPosition(0, new Vector3(lazerPos.x, lazerPos.y + lazerOffset, 0f));
                lr.SetPosition(1, Vector3.up * 200f);
            }
        }
        
    }

    IEnumerator ShootBool() 
    { 
        lr.positionCount = 2;
        shooting = true;
        yield return new WaitForSeconds(lazerDuration);
        lr.positionCount = 0;
        shooting = false; 
    }

    private void FixedUpdate() {
        if(this.direction != Vector2.zero) {
            this.rb.AddForce(this.direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball != null) 
        {
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = other.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = other.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
        }
    }

    IEnumerator Long(int duration) 
    {
        if(!gameManager.actives[0]) 
        {
            gameManager.actives[0] = true;
            this.transform.localScale = new Vector3(transform.localScale.x * 1.5f, 1f, 1f);          
        }

        yield return new WaitForSeconds(duration); 
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        gameManager.actives[0] = false;
    }

    IEnumerator Inverse(int duration) 
    {
        if(!gameManager.actives[3]) 
        {
            gameManager.actives[3] = true;       
        }
        yield return new WaitForSeconds(duration);
        gameManager.actives[3] = false;
    }

    IEnumerator Short(int duration) 
    {
        if(!gameManager.actives[5]) 
        {
            gameManager.actives[5] = true;
            this.transform.localScale = new Vector3(transform.localScale.x / 1.5f, 1f, 1f);          
        }

        yield return new WaitForSeconds(duration); 
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        gameManager.actives[5] = false;
    }

    public void Lazer(int ammo) 
    {
        this.lazerAmmo += ammo;      
    }

    
}

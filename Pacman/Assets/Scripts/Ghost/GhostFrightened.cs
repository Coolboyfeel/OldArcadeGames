using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavoir 
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool eaten {get; private set;} 

    public override void Enable(float duration) {
        base.Enable(duration);

        if(!eaten) {
            this.body.enabled = false;
            this.eyes.enabled = false;
            this.blue.enabled = true;
            this.white.enabled = true;
        }
        

        Invoke("Flash", duration / 2.0f);
    }

    public override void Disable() {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash() {
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = false;
        this.white.enabled = true;
        this.white.GetComponent<AnimatedSprite>().Restart();
    }

    private void Eaten() {
        
        this.eaten = true;

        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;
        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;

        
    }
    private void OnEnable() {
        this.ghost.movement.multiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable() {
        this.ghost.movement.multiplier = 1.0f;
        this.eaten = false;    
    }

    void OnCollisionEnter2D(Collision2D collision) 
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) 
		{
			if(this.enabled) 
			{
				Eaten();
			} 
		}
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        Node node = other.GetComponent<Node>();
        
        if(node != null && this.enabled) 
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections) 
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if(distance > maxDistance) 
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }    
            }
            
            this.ghost.movement.SetDirection(direction);
        }
    }
}    
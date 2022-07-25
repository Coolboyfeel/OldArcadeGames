using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{    
    public SpriteRenderer sr;
    public Movement movement;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;


    void Awake() 
    {
        this.sr = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    void Update() 
    {
        if (this.movement.direction == Vector2.up) {
            this.sr.sprite = this.up;
        }
        else if (this.movement.direction == Vector2.down) {
            this.sr.sprite = this.down;
        }
        else if (this.movement.direction == Vector2.left) {
            this.sr.sprite = this.left;
        }
        else if (this.movement.direction == Vector2.right) {
            this.sr.sprite = this.right;
        } 
    }
}
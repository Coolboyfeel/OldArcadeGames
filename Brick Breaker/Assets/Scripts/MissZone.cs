using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZone : MonoBehaviour
{
    public GameManager gameManager {get; private set;} 

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ball") 
        {
            gameManager.Miss();
        }
    }
}

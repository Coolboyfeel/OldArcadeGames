using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidSpawner : MonoBehaviour
{
    public GameObject astroid;
    public SpriteRenderer sr;

    public float astroidDelay;
    public float astroidDelayRemover;
    public int x;
    public int y;
    public int sideOrUp;
    public int rotation;
    public bool gameOver;
    public GameObject player;

    void Awake()
    { 
        PickRandoms();
    }

    void Update() 
    {
        gameOver = player.GetComponent<player>().gameOver;
    } 

    void Spawn()
    {
        if (gameOver == false) 
        {
            Instantiate(astroid, transform.position, transform.rotation);
            Invoke("PickRandoms", 0f);
        }
        if (astroidDelay > 2f) 
        {
            astroidDelay = astroidDelay - astroidDelayRemover;
        }   
    }

    void PickRandoms()
    {
        sideOrUp = Random.Range(1, 10);
        if (sideOrUp < 5)
        {
            x = Random.Range(1, 10);
            y = Random.Range(12, -12);
            if (x < 5) 
            {
                this.transform.position = new Vector3
                (7.2f, y, 0.0f);
            }
            else if (x > 5) 
            {
                this.transform.position = new Vector3
                (-7.2f, y, 0.0f);
            }
            else 
            {
                this.transform.position = new Vector3
                (7.2f, y, 0.0f);
            }

        }
        else if (sideOrUp > 5)
        {
            y = Random.Range(1, 10);
            if (y < 5)
            {
                this.transform.position = new Vector3
                    (x, 5f, transform.position.z);
            }
            else if (y > 5)
            {
                this.transform.position = new Vector3
                    (x, -5f, transform.position.z); ;
            }
            else
            {
                this.transform.position = new Vector3
                   (x, 5f, transform.position.z);
            }
        }
        else
        {
            x = Random.Range(1, 10);
            y = Random.Range(12, -12);
            if (x < 5)
            {
                this.transform.position = new Vector3
                (7.2f, y, 0.0f);
            }
            else if (x > 5)
            {
                this.transform.position = new Vector3
                (-7.2f, y, 0.0f);
            }
            else
            {
                this.transform.position = new Vector3
                (7.2f, y, 0.0f);
            }
        }

        if (gameOver == false) 
        {
            Invoke("Spawn", astroidDelay);
        }
    }
}

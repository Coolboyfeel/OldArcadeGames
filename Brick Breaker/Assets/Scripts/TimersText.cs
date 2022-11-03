using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimersText : MonoBehaviour
{
    public GameManager gameManager;
    public RectTransform rt;
    public Vector2[] positions;
    public int powerupsEnabled = 0;
    private int previousNumber;
    public string thisText;
    public float delay;
    public bool locked = false;


    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        rt = GetComponent<RectTransform>();
        
        //Invoke("SetPosition", delay);
    }

    void OnEnable() {
        locked = false;
        powerupsEnabled = 0;
        if(gameManager != null) {
            for (int i = 0; i < gameManager.actives.Length; i++) {
                if(gameManager.actives[i] == true) {
                    powerupsEnabled++;
                } 
            }
        }
        
        previousNumber = powerupsEnabled;
    }

    void Update() {
        if(!locked && powerupsEnabled > 0) {
            rt.anchoredPosition  = positions[powerupsEnabled - 1];
            locked = true;
        }
              
    }
    
    //void FixedUpdate() {
    //powerupsEnabled = 0;
    //    if(gameManager != null) {
      //      for (int i = 0; i < gameManager.actives.Length; i++) {
        //        if(gameManager.actives[i] == true) {
          //          powerupsEnabled++;
            //    } 
            //}
        //}
    
   //     if(powerupsEnabled < previousNumber && rt.anchoredPosition != positions[0]) {
     //       previousNumber = powerupsEnabled;
       //     rt.anchoredPosition  = positions[previousNumber - 2];       
        //} 
    //}
}

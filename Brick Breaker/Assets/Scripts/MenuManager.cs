using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public float startDelay;
    public bool hovering = false;
    
    
    [Header("Animators Main")]
    public Animator startAnim;
    public Animator optionsButAnim;
    public Animator backButAnim;

    [Header("Audio")]
    public FMODUnity.EventReference buttonSelectEvent;
    public FMOD.Studio.EventInstance buttonSelect;

    private void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ToggleControls() 
    {
        gameManager.ToggleKeys();
    }

    public void StartGame() 
    {
        startAnim.Play("Click");
        gameManager.NewGame();
    }

    public void MainMenu() 
    {
        gameManager.BackToMenu();
    }

    public void OnClick() 
    {
        hovering = false;
    }
    
    public void HoveringAudio() {
        if(!hovering) 
        {
            buttonSelect = FMODUnity.RuntimeManager.CreateInstance(buttonSelectEvent);
            buttonSelect.start();
        }
    }

    public void StartButHovering()
    {
        if(!hovering) 
        {
            startAnim.Play("HoverEnter");
            hovering = true;
        }
        else if(hovering) 
        {
            startAnim.Play("HoverExit");
            hovering = false;
        }
    }

    public void OptionsButHovering() 
    {
        if(!hovering) 
        {   
            optionsButAnim.Play("HoverEnter");
            hovering = true;
        }
        else if(hovering) 
        {
            optionsButAnim.Play("HoverExit");
            hovering = false;
        }
    }

    public void BackButHovering() 
    {
        if(!hovering) 
        {
            backButAnim.Play("HoverEnter");
            hovering = true;
        }
        else if(hovering) 
        {
            backButAnim.Play("HoverExit");
            hovering = false;
        }
    }
    
}

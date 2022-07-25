using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehavoir : MonoBehaviour
{
    public Ghost ghost { get; private set; }

    public float duration;

    void Awake() 
    {
        ghost = GetComponent<Ghost>();
        enabled = false;
    }

    public void Enable() 
    {
        Enable(duration);
    }

    public virtual void Enable(float duration) 
    {
        enabled = true;

        CancelInvoke();
        Invoke(("Disable"), duration);
    }

    public virtual void Disable() 
    {
        enabled = false;

        CancelInvoke();
    }
}

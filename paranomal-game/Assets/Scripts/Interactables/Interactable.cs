using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Add or remove an InteractionEvent component to gameObject
    public bool useEvents;
    // Displays message to player when looking at an interactable component
    public string promptMessage; 

    // This function gets called from the player
    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }

        Interact();
    }

    protected virtual void Interact()
    {
        // Template function to be overridden by subclassess
    }
}

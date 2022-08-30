using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage; // Displays message to player when looking at an interactable component

    // This function gets called from the player
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        // Template function to be overridden by subclassess
    }
}

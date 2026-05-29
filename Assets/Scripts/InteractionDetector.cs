using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private InteractableI interacatableInRange = null;
    public GameObject interactionIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);   
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interacatableInRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractableI interactable) && interactable.CanInteract())
        {
            interacatableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractableI interactable) && interactable == interacatableInRange)
        {
            interacatableInRange = null;
            interactionIcon.SetActive(false);
        }
    }

}

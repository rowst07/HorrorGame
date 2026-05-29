using UnityEngine;

public class KeyPickup : MonoBehaviour, InteractableI
{
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        GameManager.instance.keysCollected++;

        Debug.Log("Key collected: " + GameManager.instance.keysCollected);
        AudioManager.instance.PlaySFX(AudioManager.instance.keyPickup);

        Destroy(gameObject);
    }
}
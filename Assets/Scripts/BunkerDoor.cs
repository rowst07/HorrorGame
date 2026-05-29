using UnityEngine;
using UnityEngine.SceneManagement;

public class BunkerDoor : MonoBehaviour, InteractableI
{
    private bool isUnlocked = false;
    private bool isOpened = false;

    [Header("Visual States")]
    [SerializeField] private GameObject lockedVisual;
    [SerializeField] private GameObject closedVisual;
    [SerializeField] private GameObject openVisual;

    private void Start()
    {
        lockedVisual.SetActive(true);
        closedVisual.SetActive(false);
        openVisual.SetActive(false);
    }

    public void Interact()
    {
        if (isOpened) return;

        if (!isUnlocked)
        {
            TryUnlock();
        }
        else
        {
            OpenDoor();
        }
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    private void TryUnlock()
    {
        if (GameManager.instance.keysCollected >= 3)
        {
            UnlockDoor();
        }
        else
        {
            Debug.Log("Need 3 keys!");
            AudioManager.instance.PlaySFX(AudioManager.instance.doorClose);
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;

        lockedVisual.SetActive(false);
        closedVisual.SetActive(true);

        AudioManager.instance.PlaySFX(AudioManager.instance.keyPickup);
    }

    private void OpenDoor()
    {
        isOpened = true;

        closedVisual.SetActive(false);
        openVisual.SetActive(true);

        AudioManager.instance.PlaySFX(AudioManager.instance.doorOpen);

        EnterBunker();
    }

    private void EnterBunker()
    {
        Debug.Log("YOU WIN");
    }
}
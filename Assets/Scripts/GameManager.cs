using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int keysCollected;

    private void Awake()
    {
        instance = this;
    }
}
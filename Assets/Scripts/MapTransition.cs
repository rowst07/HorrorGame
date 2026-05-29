using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private Collider2D mapBoundary;

    private CinemachineConfiner2D confiner;
    [SerializeField] private Direction direction;
    [SerializeField] Transform teleportTargetPosition;
    [SerializeField] float additivePos = 5f;

    enum Direction { Up, Down, Left, Right, Teleport }

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
            confiner.InvalidateBoundingShapeCache();

            HandleAudioTransition();
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += additivePos;
                break;
            case Direction.Down:
                newPos.y -= additivePos;
                break;
            case Direction.Left:
                newPos .x -= additivePos;
                break;
            case Direction.Right:
                newPos.x += additivePos;
                break;
            case Direction.Teleport:
                player.transform.position = teleportTargetPosition.position;
                return;
        }

        

        player.transform.position = newPos;
    }

    private void HandleAudioTransition()
{
    switch (direction)
    {
        case Direction.Up:
        case Direction.Down:
        case Direction.Left:
        case Direction.Right:
            AudioManager.instance.PlayAmbience(
                AudioManager.instance.graveyardAmbience
            );
            break;

        case Direction.Teleport:
            // aqui vais controlar interior vs exterior manualmente
            AudioManager.instance.PlayAmbience(
                AudioManager.instance.bunkerAmbience
            );
            break;
    }
}
}
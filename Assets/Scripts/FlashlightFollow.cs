using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public PlayerMovement player;
    public Transform flashlight;

    void Update()
    {
        Vector2 dir = player.GetMoveDirection();

        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            flashlight.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
public Transform player;

    void LateUpdate()
    {
        // Follow player position but maintain fixed height
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // Optional: Follow player rotation
        // transform.rotation = Quarternion.Euler(90f, player.eulerAngles.y, 0f);
    }

}
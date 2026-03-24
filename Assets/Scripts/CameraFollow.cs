using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    float highestY;

    void Start()
    {
        // Track the highest Y position reached for scoring
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Get midpoint
        float midpoint = transform.position.y;

        // Only move camera up when player is above midpoint
        if (player.position.y > midpoint)
        {
            float targetY = player.position.y;
            
            // Set new camera Y to a point between current Y and target Y based on smoothSpeed
            float newY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);

            float travelled = newY - highestY;

            // Add score based on how much we've moved up since the last highest point
            if (travelled > 0)
            {
                GameManager.Instance?.AddScore(Mathf.RoundToInt(travelled * 10f));
                highestY = newY;
            }

            transform.position = new Vector3(0f, newY, transform.position.z);
        }
    }
}
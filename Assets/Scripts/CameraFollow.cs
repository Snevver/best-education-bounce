using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private float highestY;

    void Start() {
        highestY = transform.position.y;
    }

    void Update() {
        if (player == null) return;

        if (player.position.y > transform.position.y) {
            float newY = Mathf.Lerp(transform.position.y, player.position.y, smoothSpeed * Time.deltaTime);

            float travelled = newY - highestY;
            
            if (travelled > 0) {
                GameManager.Instance?.AddScore(Mathf.RoundToInt(travelled * 15f));
                highestY = newY;
            }

            transform.position = new Vector3(0f, newY, transform.position.z);
        }
    }
}
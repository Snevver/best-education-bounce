using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce = 54f;
    public float floatSpeed = 1.5f;
    public float floatHeight = 0.3f;

    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        // Make the boost float up and down
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    // Handle collision with the player
    void OnTriggerEnter2D(Collider2D col) {
        // Check if the collided object is the player
        if (!col.gameObject.CompareTag("Player")) return;

        // Get the PlayerController component and apply the boost
        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        player.Bounce(boostForce);
        player.PlayBoostBounceSfx();
        Destroy(gameObject);

        GameManager.Instance.AddScore(250);
    }
}
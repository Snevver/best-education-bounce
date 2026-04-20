using UnityEngine;

public class Platform : MonoBehaviour
{
    public float bounceForce = 18f;

    const float topHitTolerance = 0.08f;

    // Helper method to determine if the collision is a valid top hit for bouncing
    private bool IsTopHit(Collision2D col, Rigidbody2D rb) {
        // Only allow bouncing if the player is falling and the collision is from above
        if (rb == null || rb.linearVelocity.y > 0.5f) return false;
        if (col.collider == null || col.otherCollider == null) return false;

        // Calculate the top of the platform and the bottom of the player's body to determine if it's a valid bounce
        float platformTop = col.otherCollider.bounds.max.y;
        float bodyBottom = col.collider.bounds.min.y;
        return bodyBottom >= platformTop - topHitTolerance;
    }

    // Handle collisions with the player and monsters
    void OnCollisionEnter2D(Collision2D col) {
        // Check if the collided object is the player
        if (col.gameObject.CompareTag("Player")) {
            // Get the player's Rigidbody2D to check if it's a valid top hit for bouncing
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (!IsTopHit(col, rb)) return;
            PlayerController player = col.gameObject.GetComponent<PlayerController>();

            // Apply the bounce force to the player and play the bounce sound effect
            player.Bounce(bounceForce);
            player.PlayNormalPlatformBounceSfx();
        }
        
        // Check if the collided object is a monster
        if (col.gameObject.CompareTag("Monster")) {
            // Get the monster's Rigidbody2D to check if it's a valid top hit for bouncing
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (!IsTopHit(col, rb)) return;

            // Apply a smaller bounce force to the monster
            col.gameObject.GetComponent<MonsterController>().Bounce(bounceForce / 4f);
        }
    }
}
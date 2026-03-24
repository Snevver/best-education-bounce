using UnityEngine;

public class Platform : MonoBehaviour
{
    public float bounceForce = 18f;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb.linearVelocity.y > 0.5f) return;

        col.gameObject.GetComponent<PlayerController>().Bounce(bounceForce);
    }
}
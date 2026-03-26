using UnityEngine;

public class Platform : MonoBehaviour
{
    public float bounceForce = 18f;

    const float topHitTolerance = 0.08f;

    bool IsTopHit(Collision2D col, Rigidbody2D rb)
    {
        if (rb == null || rb.linearVelocity.y > 0.5f) return false;
        if (col.collider == null || col.otherCollider == null) return false;

        float platformTop = col.otherCollider.bounds.max.y;
        float bodyBottom = col.collider.bounds.min.y;
        return bodyBottom >= platformTop - topHitTolerance;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (!IsTopHit(col, rb)) return;
            col.gameObject.GetComponent<PlayerController>().Bounce(bounceForce);
        }

        if (col.gameObject.CompareTag("Monster"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (!IsTopHit(col, rb)) return;
            col.gameObject.GetComponent<MonsterController>().Bounce(bounceForce / 4f);
        }
    }
}
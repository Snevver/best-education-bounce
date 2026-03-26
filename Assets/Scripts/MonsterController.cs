using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;
    float baseScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScaleX = Mathf.Abs(transform.localScale.x);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
            else
                return;
        }

        Vector3 scale = transform.localScale;
        scale.x = player.position.x < transform.position.x ? baseScaleX : -baseScaleX;
        transform.localScale = scale;
    }

    public void Bounce(float force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
    }
}
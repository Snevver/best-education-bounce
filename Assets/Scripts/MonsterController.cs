using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private float baseScaleX;
    private Rigidbody2D rb;
    private Transform player;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        baseScaleX = Mathf.Abs(transform.localScale.x);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) player = playerObject.transform;
    }

    void Update() {
        if (player == null) {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null) {
                player = playerObject.transform;
            } else {
                return;
            }
        }

        // Flip the monster to face the player
        Vector3 scale = transform.localScale;
        scale.x = player.position.x < transform.position.x ? baseScaleX : -baseScaleX;
        transform.localScale = scale;

        destroyOffscreen();
    }

    // Method to apply a bounce force to the monster
    public void Bounce(float force) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
    }

    // Destroy monsters that fall off screen
    private void destroyOffscreen() {
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        foreach (var monster in FindObjectsByType<MonsterController>(FindObjectsSortMode.None)) {
            if (monster.transform.position.y < cameraBottom - 1f) Destroy(monster.gameObject);
        }
    }
}
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce = 54f;
    public float floatSpeed = 1.5f;
    public float floatHeight = 0.3f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        player.Bounce(boostForce);
        player.PlayBoostBounceSfx();
        Destroy(gameObject);

        GameManager.Instance.AddScore(250);
    }
}
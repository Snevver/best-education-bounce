using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public GameObject explosionPrefab;
    public float monsterStompBoost = 22f;
    public AudioClip normalPlatformBounceSfx;
    public AudioClip boostBounceSfx;
    public AudioClip monsterBounceSfx;
    public AudioClip monsterDeathSfx;
    public AudioClip fallOutSfx;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    float screenHalfWidth;
    const float stompTopTolerance = 0.1f;

    Rigidbody2D rigidBody;
    Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        float input = 0f;

        // Movement with A and D keys
        if (Input.GetKey(KeyCode.A))  input = -1f;
        if (Input.GetKey(KeyCode.D)) input =  1f;

        rigidBody.linearVelocity = new Vector2(input * moveSpeed, rigidBody.linearVelocity.y);

        checkScreenCrossover();

        if (animator != null) animator.SetFloat("speedX", Mathf.Abs(input));

        // Flip sprite based on direction
        if (input < 0) transform.localScale = new Vector3(1, 1, 1);
        if (input > 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Bounce(float force, bool isPLayer = true)
    {
        if (!isPLayer) force /= 2f;
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, force);
    }

    public void PlayNormalPlatformBounceSfx()
    {
        PlaySfx(normalPlatformBounceSfx);
    }

    public void PlayBoostBounceSfx()
    {
        PlaySfx(boostBounceSfx);
    }

    public void PlayMonsterBounceSfx()
    {
        PlaySfx(monsterBounceSfx);
    }

    public void PlayFallOutSfx()
    {
        PlaySfx(fallOutSfx);
    }

    void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, transform.position, sfxVolume);
    }

    void checkScreenCrossover()
    {
        Vector3 pos = transform.position;
        if (pos.x > screenHalfWidth)  pos.x = -screenHalfWidth;
        if (pos.x < -screenHalfWidth) pos.x =  screenHalfWidth;
        transform.position = pos;
    }

    void LateUpdate()
    {
        float bottomEdge = Camera.main.transform.position.y - Camera.main.orthographicSize;
        
        if (transform.position.y < bottomEdge)
        {
            PlayFallOutSfx();
            GameManager.LastScore = GameManager.Score;
            GameManager.Instance.GameOver();
            GameManager.Score = 0;

            FadeOut fadeOut = FindObjectOfType<FadeOut>();
            fadeOut.StartFade();

            enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Monster")) return;

        if (IsStompOnMonster(col))
        {
            if (explosionPrefab != null)
                Instantiate(explosionPrefab, new Vector3(col.transform.position.x, col.transform.position.y, -1f), Quaternion.identity);
            
            Bounce(monsterStompBoost);
            PlayMonsterBounceSfx();
            Destroy(col.gameObject);

            GameManager.Instance.AddScore(500);
            return;
        }

        // Spawn explosion at player position
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity);

        PlaySfx(monsterDeathSfx);

        GameManager.LastScore = GameManager.Score;
        GameManager.Instance.GameOver();
        GameManager.Score = 0;  

        FadeOut fadeOut = FindObjectOfType<FadeOut>();
        if (fadeOut != null)
            fadeOut.StartFade();

        Destroy(gameObject);
    }

    // Really complicated check made by AI to determine if player is stomping on monster
    bool IsStompOnMonster(Collision2D col)
    {   
        if (rigidBody == null) return false;
        if (col.collider == null || col.otherCollider == null) return false;

        // In Player callbacks: otherCollider is the player's collider, collider is the monster's collider.
        Collider2D playerCollider = col.otherCollider;
        Collider2D monsterCollider = col.collider;

        bool movingDown = rigidBody.linearVelocity.y < -0.15f || col.relativeVelocity.y < -0.25f;
        if (!movingDown) return false;

        float playerBottom = playerCollider.bounds.min.y;
        float monsterTop = monsterCollider.bounds.max.y;
        bool nearTop = playerBottom >= monsterTop - stompTopTolerance;
        bool playerAboveMonster = playerCollider.bounds.center.y > monsterCollider.bounds.center.y;
        float maxHorizontalOffset = monsterCollider.bounds.extents.x * 0.8f;
        bool horizontallyCentered = Mathf.Abs(playerCollider.bounds.center.x - monsterCollider.bounds.center.x) <= maxHorizontalOffset;

        if (nearTop && playerAboveMonster && horizontallyCentered) return true;

        // Fallback for imperfect collider shapes: allow only contacts near monster top while player is above it.
        for (int i = 0; i < col.contactCount; i++)
        {
            ContactPoint2D cp = col.GetContact(i);
            bool contactNearMonsterTop = cp.point.y >= monsterTop - stompTopTolerance;
            bool contactFromAbove = cp.normal.y > 0.45f;
            if (playerAboveMonster && horizontallyCentered && contactNearMonsterTop && contactFromAbove) return true;
        }

        return false;
    }
}
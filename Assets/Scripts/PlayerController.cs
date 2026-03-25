using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;

    float screenHalfWidth;

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

    public void Bounce(float force)
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, force);
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
            GameManager.Score = 0;
            GameManager.Instance.GameOver();

            FadeOut fadeOut = FindObjectOfType<FadeOut>();
            if (fadeOut != null)
                fadeOut.StartFade();
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

            enabled = false;
        }
    }
}
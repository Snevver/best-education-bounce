using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;

    float screenHalfWidth;

    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}
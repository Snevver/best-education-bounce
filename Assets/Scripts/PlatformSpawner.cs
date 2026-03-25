using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float platformWidth = 2f;

    float nextSpawnY;
    float screenHalfWidth;

    void Start()
    {
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        nextSpawnY = Camera.main.transform.position.y - Camera.main.orthographicSize + 2f;
    }

    void Update()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;

        while (nextSpawnY < cameraTop + 10f) {
            SpawnPlatform(nextSpawnY);
            nextSpawnY += GetGap();
        }

        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        
        foreach (var platform in FindObjectsByType<Platform>(FindObjectsSortMode.None)){
            if (platform.transform.position.y < cameraBottom - 2f) Destroy(platform.gameObject);
        }
    }

    float GetGap()
    {
        int score = GameManager.Score;
        if (score < 1000) return Random.Range(1.8f, 2.5f);
        if (score < 3000) return Random.Range(2.5f, 3.5f);
        if (score < 6000) return Random.Range(3.5f, 5f);
        return Random.Range(5f, 7f);
    }

    void SpawnPlatform(float y)
    {
        // Spawn platform at random x position within the screen
        float x = Random.Range(-screenHalfWidth + platformWidth, screenHalfWidth - platformWidth);
        GameObject go = Instantiate(platformPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
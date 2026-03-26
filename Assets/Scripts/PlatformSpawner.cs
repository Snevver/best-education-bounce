using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject monsterPrefab;
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

        while (nextSpawnY < cameraTop + 10f)
        {
            SpawnPlatform(nextSpawnY);
            nextSpawnY += GetGap();
        }

        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        
        foreach (var platform in FindObjectsByType<Platform>(FindObjectsSortMode.None))
        {
            if (platform.transform.position.y < cameraBottom - 1f) Destroy(platform.gameObject);
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
        float x = Random.Range(-screenHalfWidth + platformWidth, screenHalfWidth - platformWidth);
        GameObject platform = Instantiate(platformPrefab, new Vector3(x, y, 0), Quaternion.identity);

        if (monsterPrefab != null && ShouldSpawnMonster())
        {
            float platformHeight = platform.GetComponent<SpriteRenderer>().bounds.size.y;
            float monsterX = Random.Range(x - platformWidth / 2f, x + platformWidth / 2f);
            Vector3 monsterPos = new Vector3(monsterX, y + platformHeight, 0);
            Instantiate(monsterPrefab, monsterPos, Quaternion.identity);
        }
    }

    bool ShouldSpawnMonster()
    {
        int score = GameManager.Score;
        float chance;
        if (score < 500)  chance = 0f;
        if (score < 750) chance = 0.05f;
        else if (score < 1000) chance = 0.10f;
        else if (score < 2000) chance = 0.20f;
        else chance = 0.35f;
        return Random.value < chance;
    }
}
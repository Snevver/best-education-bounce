using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    public GameObject boostPrefab;
    public float boostWidth = 2f;
    public float spawnChance = 0.15f;

    float nextSpawnY;
    float screenHalfWidth;

    void Start()
    {
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        nextSpawnY = Camera.main.transform.position.y - Camera.main.orthographicSize + 4f;
    }

    void Update()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;

        while (nextSpawnY < cameraTop + 10f) {
            if (Random.value < spawnChance) SpawnBoost(nextSpawnY);
            nextSpawnY += Random.Range(6f, 12f);
        }

        // Destroy boosts that fall off screen
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        foreach (var boost in FindObjectsByType<Boost>(FindObjectsSortMode.None)) {
            if (boost.transform.position.y < cameraBottom - 2f) Destroy(boost.gameObject);
        }
    }

    void SpawnBoost(float y)
    {
        float x = Random.Range(-screenHalfWidth + boostWidth, screenHalfWidth - boostWidth);
        Instantiate(boostPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
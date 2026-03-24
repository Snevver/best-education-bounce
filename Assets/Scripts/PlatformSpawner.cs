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

        // Spawn platforms until we have enough above the camera
        while (nextSpawnY < cameraTop + 10f)
        {
            SpawnPlatform(nextSpawnY);

            // Space out platforms randomly between 1.8 and 2.5 units
            nextSpawnY += Random.Range(1.8f, 2.5f);
        }

        // Destroy platforms that have fallen off screen
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        
        foreach (var platform in FindObjectsByType<Platform>(FindObjectsSortMode.None))
        {
            // Destroy platforms that are more than 2 units below the bottom of the camera
            if (platform.transform.position.y < cameraBottom - 2f) Destroy(platform.gameObject);
        }
    }

    void SpawnPlatform(float y)
    {
        // Spawn platform at random x position within the screen
        float x = Random.Range(-screenHalfWidth + platformWidth, screenHalfWidth - platformWidth);
        GameObject go = Instantiate(platformPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
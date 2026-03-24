using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
    }
}
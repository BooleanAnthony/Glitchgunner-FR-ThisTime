using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxStrength = 0.5f;
    public Camera cam;

    private Vector3 startCamPos;
    private Vector3 startPos;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        startCamPos = cam.transform.position;
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 camTravel = cam.transform.position - startCamPos;
        Vector3 newPos = startPos + new Vector3(camTravel.x * parallaxStrength, camTravel.y * parallaxStrength, 0);
        transform.position = newPos;
    }
}

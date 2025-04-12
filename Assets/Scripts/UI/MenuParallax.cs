using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] public float offsetMult = 1f;
    [SerializeField] public float smoothing = 0.5f;
    private Vector2 startPos;
    private Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPos + (offset * offsetMult), ref velocity, smoothing);
    }
}

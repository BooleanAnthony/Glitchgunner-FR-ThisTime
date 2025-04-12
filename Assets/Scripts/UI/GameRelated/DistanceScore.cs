using UnityEngine;
using TMPro;

public class DistanceScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private float distanceTraveled = 0f;
    private float startX;

    void Start()
    {
        startX = transform.position.x;  // Store starting X position
    }

    void Update()
    {
        float distanceTraveled = transform.position.x - startX;  // Only track X-axis
        scoreText.text = "Score: " + Mathf.FloorToInt(distanceTraveled).ToString();
    }
}
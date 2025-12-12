using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [SerializeField] private float startingDifficulty = 1f;
    [SerializeField] private float increaseAmount = 1f;
    [SerializeField] private float difficultyInterval = 20f;
    [SerializeField] private float maxDifficulty = 5f;

    private float timer = 0f;
    public float currentDifficulty;

    public float CurrentDifficulty => currentDifficulty;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentDifficulty = startingDifficulty;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= difficultyInterval && currentDifficulty < maxDifficulty)
        {
            currentDifficulty += increaseAmount;
            currentDifficulty = Mathf.Min(currentDifficulty, maxDifficulty);
            timer = 0f;

            Debug.Log("Difficulty increased to: " + currentDifficulty);
        }
    }
}

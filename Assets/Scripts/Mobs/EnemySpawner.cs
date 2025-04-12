using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRateInSeconds;
    [SerializeField] public GameObject EnemyDasher;
    [SerializeField] public GameObject EnemyShooter;

    private GameObject[] enemies = new GameObject[2];
    private GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SpawnEnemy), maxSpawnRateInSeconds);
        enemies = new GameObject[] { EnemyDasher, EnemyShooter};
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.7f, 1));

        if (enemies.Length > 0) // Ensure array is not empty
        {
            enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy);
        }
        enemy.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
    
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else 
        {
            spawnInSeconds = 1f;
        }

        Invoke(nameof(SpawnEnemy), spawnInSeconds);
    }
}

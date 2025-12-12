using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRateInSeconds;
    [SerializeField] public GameObject[] enemies;
    [SerializeField] Vector2 minSpawnPosition; // Serialized min position
    [SerializeField] Vector2 maxSpawnPosition; // Serialized max position

    private GameObject enemy;

    IEnumerator Start()
    {
        yield return null;
        Invoke(nameof(SpawnEnemy), maxSpawnRateInSeconds);
    }
    
    void SpawnEnemy()
    {
        if (enemies.Length > 0)
        {
            enemy = enemies[Random.Range(0, enemies.Length)];
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.transform.position = new Vector2(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );
        }

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds = maxSpawnRateInSeconds > 1f
            ? Random.Range(1f, maxSpawnRateInSeconds)
            : 1f;

        Invoke(nameof(SpawnEnemy), spawnInSeconds);
    }
}

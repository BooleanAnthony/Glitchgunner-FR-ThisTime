using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class SpawnedEnemy
    {
        public Transform enemyReference;
        public Vector3 targetPos;

        public SpawnedEnemy(Transform enemy, Vector2 target)
        {
            enemyReference = enemy;
            targetPos = target;
        }

        public bool CheckIfNotInPos() //checks if the enemy is on the spawnpoint, return true if not
        {
            return enemyReference.position != targetPos;
        }

        public void ToggleScripts(bool value)
        {
            MonoBehaviour[] scripts = enemyReference.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = value;
            }
        }
    }

    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpawnRateInSeconds;
    [SerializeField] public GameObject[] enemies;
    [SerializeField] float backSpawnPosition;
    [SerializeField] Vector2 minSpawnPosition; // Serialized min position
    [SerializeField] Vector2 maxSpawnPosition; // Serialized max position
    public List<SpawnedEnemy> spawnedEnemiesNotInPosition;

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
            Vector2 targetPos = new(
                UnityEngine.Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                UnityEngine.Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );
            enemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.transform.position = new(
                backSpawnPosition,
                targetPos.y
            ); //spawns enemy at back

            SpawnedEnemy newSpawn = new(spawnedEnemy.transform, targetPos);
            newSpawn.ToggleScripts(false);
            spawnedEnemiesNotInPosition.Add(newSpawn);
        }

        ScheduleNextEnemySpawn();
    }

    void Update()
    {
        float step =  movementSpeed * Time.deltaTime; // calculate distance to move
        foreach (SpawnedEnemy spawnedEnemy in spawnedEnemiesNotInPosition)
        {
            if (spawnedEnemy.CheckIfNotInPos())
            {
                spawnedEnemy.enemyReference.position = Vector3.MoveTowards(spawnedEnemy.enemyReference.position, spawnedEnemy.targetPos, step);
            }
            else
            {
                spawnedEnemiesNotInPosition.Remove(spawnedEnemy);
                spawnedEnemy.ToggleScripts(true);
            }
        }
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds = maxSpawnRateInSeconds > 1f
            ? UnityEngine.Random.Range(1f, maxSpawnRateInSeconds)
            : 1f;

        Invoke(nameof(SpawnEnemy), spawnInSeconds);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetEnemySpawner : MonoBehaviour
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
    [SerializeField] public Transform[] initialSpawnPoints;
    [SerializeField] Vector2 minSpawnPosition; // Serialized min position
    [SerializeField] Vector2 maxSpawnPosition; // Serialized max position
    public List<SpawnedEnemy> spawnedEnemiesNotInPosition;
    public bool isActive = false;

    private GameObject enemy;

    IEnumerator Start()
    {
        yield return null;
        Invoke(nameof(SpawnEnemy), maxSpawnRateInSeconds);
    }
    
    void SpawnEnemy()
    {
        ScheduleNextEnemySpawn();
        if (!isActive)
        {
            return;
        }

        if (enemies.Length > 0)
        {
            Vector2 targetPos = new(
                UnityEngine.Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                UnityEngine.Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );
            enemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];
            Vector2 backSpawnPosition = initialSpawnPoints[UnityEngine.Random.Range(0, initialSpawnPoints.Length)].position;
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.transform.position = backSpawnPosition; //spawns enemy at spawn point

            SpawnedEnemy newSpawn = new(spawnedEnemy.transform, targetPos);
            newSpawn.ToggleScripts(false);
            spawnedEnemiesNotInPosition.Add(newSpawn);
        }
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

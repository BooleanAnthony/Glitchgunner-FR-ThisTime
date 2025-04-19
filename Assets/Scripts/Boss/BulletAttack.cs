using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    enum SpawnerType {Straight, Spin, Aim}

    [Header("BulletAttributes")]
    [SerializeField] private GameObject directionalBullet;
    [SerializeField] private float bulletLife = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private int range = 10;
    [SerializeField] private float firingSpeed = 0.5f;
    [SerializeField] private DifficultyManager difficultyManager;

    private GameObject spawnedBullet;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin) firePoint.eulerAngles = new Vector3(0f, 0f, firePoint.eulerAngles.z+1f);
        if (timer >= firingSpeed)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (directionalBullet)
        {
            spawnedBullet = Instantiate(directionalBullet, firePoint.position, Quaternion.identity);
            spawnedBullet.GetComponent<DirectionalBullet>()._age = bulletLife;

            if (spawnerType == SpawnerType.Aim && player != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;

                // Fix rotation direction by adding 180 degrees
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                firePoint.rotation = Quaternion.Euler(0f, 0f, angle); 
                spawnedBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f + Random.Range(-range + (int)difficultyManager.CurrentDifficulty, range - (int)difficultyManager.CurrentDifficulty));
            }
            else
            {
                spawnedBullet.transform.rotation = firePoint.rotation;
            }
        }
    }
}

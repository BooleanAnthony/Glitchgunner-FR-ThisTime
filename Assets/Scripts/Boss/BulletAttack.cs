using System.Collections;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    enum SpawnerType {Straight, Spin, Aim, Shotgun}

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
    [SerializeField] private int pelletCount = 5;      // Number of bullets in the shotgun blast
    [SerializeField] private float spreadAngle = 60f;  // Total spread angle (degrees)

    private GameObject spawnedBullet;
    private float timer = 0f;
    public AudioSource audioPlayer;

    IEnumerator Start()
    { 
        yield return new WaitForSeconds(3f);
    }

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
        audioPlayer.Play();
        if (directionalBullet)
        {
            if (spawnerType == SpawnerType.Aim && player != null || spawnerType == SpawnerType.Shotgun)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                if (spawnerType == SpawnerType.Aim)
                {
                    // Single aimed shot
                    firePoint.rotation = Quaternion.Euler(0f, 0f, baseAngle);
                    spawnedBullet = Instantiate(directionalBullet, firePoint.position, Quaternion.Euler(0f, 0f, baseAngle + 180f + Random.Range(-range + (int)difficultyManager.CurrentDifficulty, range - (int)difficultyManager.CurrentDifficulty)));

                    if (spawnedBullet.TryGetComponent(out DirectionalBullet bullet))
                        bullet._age = bulletLife;
                }
                else if (spawnerType == SpawnerType.Shotgun)
                {
                    // Shotgun spread
                    float halfSpread = spreadAngle / 2f;
                    for (int i = 0; i < pelletCount; i++)
                    {
                        float offset = Random.Range(-halfSpread, halfSpread);
                        float finalAngle = baseAngle + 180f + offset;
                        Quaternion rot = Quaternion.Euler(0f, 0f, finalAngle);
                        GameObject pellet = Instantiate(directionalBullet, firePoint.position, rot);

                        if (pellet.TryGetComponent(out DirectionalBullet bullet))
                            bullet._age = bulletLife;
                    }
                }
            }
            else
            {
                // Default behavior
                spawnedBullet = Instantiate(directionalBullet, firePoint.position, firePoint.rotation);

                if (spawnedBullet.TryGetComponent(out DirectionalBullet bullet))
                    bullet._age = bulletLife;
            }
        }
    }
}

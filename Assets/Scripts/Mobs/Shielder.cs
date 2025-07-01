using Environment;
using Player;
using UnityEngine;

public class Shielder : EnemyDrone
{
    [Header("Unique Stats")]
    [SerializeField] private Shield shield; 
    public float yOffset = 0f;

    private float timer = 0f;
    private Transform player;

    protected override void Awake()
    {
        base.Awake();
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene. Please ensure the player has the tag 'Player'.");
            }
        }

        if (DifficultyManager.Instance != null)
        {
            difficulty = DifficultyManager.Instance.CurrentDifficulty;
        }
        else
        {
            Debug.LogWarning("DifficultyManager instance not found. Defaulting to difficulty 1.");
        }

        DifficultyScale();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Fire bullet when the timer exceeds the firing speed
        if (timer >= firingSpeed && !deathTriggered)
        {
            anim.SetTrigger("shield");
            timer = 0f; // Reset the timer after firing
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered Collision");
        if (collision.gameObject.name.Contains("Bullet") && !collision.gameObject.name.Contains("Enemy"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.GetDamage();
            Debug.Log("enemy took damage: " + bullet.GetDamage());
            if (health <= 0)
            {
                Debug.Log("enemy died");
                if (!deathTriggered)
                {
                    anim.SetTrigger("dead");
                    collider.enabled = false;
                    deathTriggered = true;
                }
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered Player");
            DroneMovement drone = collision.gameObject.GetComponent<DroneMovement>();
            drone?.KillPlayer();
        }
    }

    protected void DifficultyScale()
    {
        firingSpeed -= difficulty;
    }

    private void ShieldUp()
    {
        shield.HealShield();
    }
}
using UnityEngine;
using Player;
using Environment;

public class Shooter : EnemyDrone
{
    [Header("Unique Stats")]
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float alignOffsetRange = 0.5f;
    public float yOffset = 0f;
    private Transform shootPoint;

    private float timer = 0f;
    private Transform player; 
    private bool inContact = false;
    
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

        if (shootPoint == null)
        {
            shootPoint = transform.Find("Firepoint");
            if (shootPoint == null)
            {
                Debug.LogError("Firepoint not found! Make sure it is named correctly and is a child of Shooter.");
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

        yOffset = Random.Range(-alignOffsetRange, alignOffsetRange);

        DifficultyScale();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Align Y position with the player
        if (!inContact)
        {
            Vector3 pos = transform.position;
            float targetY = player.position.y + yOffset;
            pos.y = Mathf.MoveTowards(pos.y, targetY, alignSpeed * Time.deltaTime);
            transform.position = pos;
        }

        // Fire bullet when the timer exceeds the firing speed
        if (timer >= firingSpeed && !deathTriggered)
        {
            anim.SetTrigger("fire");
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

        if (collision.gameObject.CompareTag("EnemyMob"))
        {
            inContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyMob"))
        {
            inContact = false;
        }
    }

    private void FireBullet()
    {
        print("Fire!");
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    protected void DifficultyScale()
    {
        float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

        alignSpeed *= dampening;
        firingSpeed /= dampening;
    }
}
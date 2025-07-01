using UnityEngine;
using Player;
using Environment;


public class Boomerang : EnemyDrone
{
    [Header("Unique Stats")]
    [SerializeField] private float alignOffsetRange = 0.5f;
    [SerializeField] private BoxCollider2D detectionBox; 
    public bool dead = false;
    public bool inContact = false;
    private bool invincible = false;
    public bool attacking = false;

    public float getFiringSpeed()
    {
        return firingSpeed;
    }
    public float getAlignSpeed()
    {
        return alignSpeed;
    }

    protected override void Awake()
    {
        base.Awake();
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

    private void OnTriggerEnter2D(Collider2D collision)
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
                    anim.SetBool("isDead", true);
                    anim.SetTrigger("dead");
                    collider.enabled = false;
                    deathTriggered = true;
                }
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Triggered Player");
            if (collision.gameObject.TryGetComponent<Health>(out var drone))
            {
                drone.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Health component not found on Player!");
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

    protected void DifficultyScale()
    {
        float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

        alignSpeed *= dampening;
        firingSpeed /= dampening;
    }

    private void KillMob()
    {
        dead = true;
    }

    private void InvincibilitySwitch()
    {
        invincible = !invincible;
    }

    private void AttackingSwitch()
    {
        attacking = !attacking;
    }
}

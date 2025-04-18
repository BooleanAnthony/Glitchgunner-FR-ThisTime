using UnityEngine;
using Player;
using Environment;

namespace Mobs
{
    public class Boomerang : MonoBehaviour
    {
        [SerializeField] private float alignSpeed = 3f; 
        [SerializeField] private float firingSpeed = 3f; 
        [SerializeField] private float health = 20; 
        [SerializeField] private int damage = 1;  
        [SerializeField] private float alignOffsetRange = 0.5f;
        [SerializeField] private BoxCollider2D detectionBox; 
        private float difficulty = 1; //multiplier for HP/Damage
        private new BoxCollider2D collider; 
        private bool deathTriggered = false;
        public Animator anim; 
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

        private void Awake()
        {

            if (DifficultyManager.Instance != null)
            {
                difficulty = DifficultyManager.Instance.CurrentDifficulty;
            }
            else
            {
                Debug.LogWarning("DifficultyManager instance not found. Defaulting to difficulty 1.");
            }

            anim = GetComponent<Animator>();
            collider = GetComponent<BoxCollider2D>();

            difficultyScale();
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

        private void difficultyScale()
        {
            health *= difficulty;
            damage = Mathf.RoundToInt(damage * difficulty);

            float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

            alignSpeed *= dampening;
            firingSpeed /= dampening;
        }

        private void KillMob()
        {
            dead = true;
        }

        private void invincibilitySwitch()
        {
            invincible = !invincible;
        }

        private void attackingSwitch()
        {
            attacking = !attacking;
        }
    }

}

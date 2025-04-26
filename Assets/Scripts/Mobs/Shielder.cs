using Environment;
using Player;
using UnityEngine;

namespace Mobs
{
    public class Shielder : MonoBehaviour
    {
        [SerializeField] private float healingCD = 15f; 
        [SerializeField] private float health = 10; 
        [SerializeField] private Shield shield; 
        public float yOffset = 0f;
        private float difficulty = 1; //multiplier for HP/Damage

        private float timer = 0f;
        private Transform player;  
        private Animator anim; 
        private new BoxCollider2D collider; 
        private bool deathTriggered = false;
        


        private void Awake()
        {
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

            anim = GetComponent<Animator>();
            collider = GetComponent<BoxCollider2D>();

            difficultyScale();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            // Fire bullet when the timer exceeds the firing speed
            if (timer >= healingCD && !deathTriggered)
            {
                anim.SetTrigger("shield");
                timer = 0f; // Reset the timer after firing
            }
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
                DroneMovement drone = collision.gameObject.GetComponent<DroneMovement>();
                drone?.KillPlayer();
            }
        }

        private void difficultyScale()
        {
            healingCD -= difficulty;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        private void ShieldUp()
        {
            shield.HealShield();
        }
    }
}

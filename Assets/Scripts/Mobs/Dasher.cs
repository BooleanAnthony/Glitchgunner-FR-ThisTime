using UnityEngine;
using Environment;

namespace EnemyMob
{
    public class Dasher : MonoBehaviour
    {
        [SerializeField] private float alignSpeed = 3f; 
        [SerializeField] private float dashSpeed = 10f;  
        [SerializeField] private float alignDuration = 4f;
        [SerializeField] private float health = 30;  
        [SerializeField] private int damage = 2;  
        [SerializeField] private float alignOffsetRange = 0.5f;
        public float yOffset = 0f;
        private float difficulty = 1; //multiplier for HP/Damage
 

        private float timer = 0f;
        private bool isDashing = false;
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

            anim = GetComponent<Animator>();
            collider = GetComponent<BoxCollider2D>();

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
            //if (deathTriggered)
            //{
            //    return;
            //}

            if (!isDashing)
            {
                Vector3 pos = transform.position;
                float targetY = player.position.y + yOffset;
                pos.y = Mathf.MoveTowards(pos.y, targetY, alignSpeed * Time.deltaTime);
                transform.position = pos;

                if (timer >= alignDuration)
                {
                    isDashing = true;

                    float direction = Mathf.Sign(player.position.x - transform.position.x);
                    dashSpeed = Mathf.Abs(dashSpeed) * direction;
                }
            }
            else
            {
                anim.SetBool("attacking", true);
                transform.position += new Vector3(dashSpeed * Time.deltaTime, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Contains("Bullet") && !collision.gameObject.name.Contains("Enemy"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    health -= bullet.GetDamage();
                    Debug.Log("enemy took damage: " + bullet.GetDamage());
                    if (health <= 0 && !isDashing)
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
        }

        private void DifficultyScale()
        {
            health *= difficulty;
            damage = Mathf.RoundToInt(damage * difficulty);

            float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

            alignSpeed *= dampening;
            dashSpeed *= dampening;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}

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
            }

            anim = GetComponent<Animator>();
            collider = GetComponent<BoxCollider2D>();

            if (DifficultyManager.Instance != null)
            {
                difficulty = DifficultyManager.Instance.CurrentDifficulty;
            }

            DifficultyScale();
        }

        private void Update()
        {
            timer += Time.deltaTime;

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
                    if (health <= 0 && !isDashing)
                    {
                        if (!deathTriggered)
                        {
                            anim.SetBool("dead", true);
                            collider.enabled = false;
                            deathTriggered = true;
                        }
                    }
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<Health>(out var drone))
                {
                    if (isDashing)
                    {
                        drone.TakeDamage(damage);
                    }
                    else 
                    {
                        drone.TakeDamage(damage - 1);
                    }
                    
                }
            }
        }

        private void DifficultyScale()
        {
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

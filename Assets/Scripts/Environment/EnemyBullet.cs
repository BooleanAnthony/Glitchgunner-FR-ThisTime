using UnityEngine;

namespace Environment
{
    public class EnemyBullet : Bullet
    {
		private void OnTriggerEnter2D(Collider2D collision) {
            if ((!collision.gameObject.CompareTag("EnemyMob") && !collision.gameObject.name.Contains("EnemyBullet")) || !collision.gameObject.CompareTag("Collider"))
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.gameObject.TryGetComponent<Health>(out var drone))
                    {
                        drone.TakeDamage(damage);
                    }
                    else
                    {
                        Debug.LogWarning("Health component not found on Player!");
                    }
                }
                Destroy(gameObject);
            }
		}
    }
}
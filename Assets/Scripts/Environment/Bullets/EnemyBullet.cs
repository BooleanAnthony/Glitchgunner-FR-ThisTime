using UnityEngine;

namespace Environment
{
    public class EnemyBullet : Bullet
    {
		private void OnTriggerEnter2D(Collider2D collision) {
            int layer = collision.gameObject.layer;
            if (layer != LayerMask.NameToLayer("Enemy"))
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
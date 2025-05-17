using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] protected int damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Enemy"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<Health>(out var drone))
                {
                    drone.TakeDamage(damage);
                }
            }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}

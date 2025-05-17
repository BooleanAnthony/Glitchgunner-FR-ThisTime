using UnityEngine;

public class CentipedeHead : MonoBehaviour
{
    [SerializeField] private int damage = 1;  

    private void OnTriggerEnter2D(Collider2D collision)
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

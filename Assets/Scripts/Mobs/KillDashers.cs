using UnityEngine;

public class KillEnemies : MonoBehaviour
{
    // This method is called when another collider enters this trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyMob"))
        {
            Destroy(collision.gameObject);
        }
    }
}

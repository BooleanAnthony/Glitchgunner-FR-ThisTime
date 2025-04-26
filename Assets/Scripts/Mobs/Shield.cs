using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float health = 3; 
    private SpriteRenderer spriteRend;
    private BoxCollider2D boxCollider;
    private bool active = true;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void HealShield()
    {
        health = 3;

        if (!active)
        {
            active = true;
            spriteRend.enabled = active;
            boxCollider.enabled = active;
        }
    }

    private void TakeDamage(float value)
    {
        health -= value;

        if (health <= 0)
        {
            active = false;
            spriteRend.enabled = active;
            boxCollider.enabled = active;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Collider"))
        {
            Debug.Log("Shield collided with: " + collision.gameObject.name);
            int layer = collision.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Player"))
            {
                TakeDamage(1);
            }
        }
    }
}

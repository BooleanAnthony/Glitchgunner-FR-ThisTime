using UnityEngine;
using Player;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float debugCurrentHealth;
    [SerializeField] private float invincibilityDuration = 1f; 
    [SerializeField] private float invincibilityTimer = 0f; 
    public float CurrentHealth { get; private set; }
    private DroneMovement playerScript;

    void Awake()
    {
        if (GameManager.instance != null)
        {
            CurrentHealth = GameManager.instance.playerHealth > 0 ? GameManager.instance.playerHealth : startingHealth;
        }
        else
        {
            CurrentHealth = startingHealth;
        }
    }

    void Update()
    {
        debugCurrentHealth = CurrentHealth;

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime; 
        }
    }

    public void TakeDamage(float _damage)
    {
        if (playerScript == null)
        {
            playerScript = Object.FindAnyObjectByType<DroneMovement>();
        }
        
        if (invincibilityTimer <= 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

            // Update GameManager so health persists across scenes
            if (GameManager.instance != null)
            {
                GameManager.instance.playerHealth = CurrentHealth;
            }

            print("Lost " + _damage + " hearts");

            if (CurrentHealth > 0)
            {
                print("Player still alive");
            }
            else
            {
                if (playerScript != null && !playerScript.dead)
                {
                    playerScript.KillPlayer();
                    print("You died");
                    playerScript.dead = true;
                }
            }

            // Reset invincibility timer **before exiting**
            invincibilityTimer = invincibilityDuration;
        }
        else
        {
            print("Player is invincible, can't take damage yet.");
        }
    }

    public void FullHeal()
    {
        if (playerScript == null)
        {
            playerScript = Object.FindAnyObjectByType<DroneMovement>();
        }
        
        CurrentHealth = startingHealth;

        // Update GameManager to reflect full heal
        if (GameManager.instance != null)
        {
            GameManager.instance.playerHealth = startingHealth;
        }
    }
}

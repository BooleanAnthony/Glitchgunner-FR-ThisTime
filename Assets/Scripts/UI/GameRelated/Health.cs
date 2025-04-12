using UnityEngine;
using Player;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private DroneMovement playerScript;

    void Awake()
    {
        // If GameManager already has a health value, use it. Otherwise, use startingHealth.
        if (GameManager.instance != null)
        {
            currentHealth = GameManager.instance.playerHealth > 0 ? GameManager.instance.playerHealth : startingHealth;
        }
        else
        {
            currentHealth = startingHealth;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float _damage)
    {
        if (playerScript == null)
        {
            playerScript = Object.FindAnyObjectByType<DroneMovement>();
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        // Update GameManager so health persists across scenes
        if (GameManager.instance != null)
        {
            GameManager.instance.playerHealth = currentHealth;
        }

        print("Lost " + _damage + " hearts"); // debug text
        if (currentHealth > 0)
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
    }

    public void FullHeal()
    {
        if (playerScript == null)
        {
            playerScript = Object.FindAnyObjectByType<DroneMovement>();
        }
        
        currentHealth = startingHealth;

        // Update GameManager to reflect full heal
        if (GameManager.instance != null)
        {
            GameManager.instance.playerHealth = startingHealth;
        }
    }
}

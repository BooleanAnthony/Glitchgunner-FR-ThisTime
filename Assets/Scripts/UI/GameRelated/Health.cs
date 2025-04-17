using UnityEngine;
using Player;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float debugCurrentHealth;
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
    }

    public void TakeDamage(float _damage)
    {
        if (playerScript == null)
        {
            playerScript = Object.FindAnyObjectByType<DroneMovement>();
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        // Update GameManager so health persists across scenes
        if (GameManager.instance != null)
        {
            GameManager.instance.playerHealth = CurrentHealth;
        }

        print("Lost " + _damage + " hearts"); // debug text
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

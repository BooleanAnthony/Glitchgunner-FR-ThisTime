using Mobs;
using UnityEngine;

public class BoomerangDetector : MonoBehaviour
{
    [SerializeField] private Boomerang boomerang; 
    private bool playerInside = false; 
    private float timer = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the detection box
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered detection box");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the player exits the detection box
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player exited detection box");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // If the player is inside the box, trigger the attack animation in the boomerang
        if (playerInside && boomerang != null)
        {
            if (timer >= boomerang.getFiringSpeed())
            {
                boomerang.anim.SetTrigger("attack"); // Trigger the attack animation
                timer = 0f; // Reset the firing timer
            }
        }
    }
}

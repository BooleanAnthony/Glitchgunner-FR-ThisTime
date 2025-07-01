using UnityEngine;

public class BoomerangHolder : MonoBehaviour
{
    [SerializeField] private Boomerang boomerang;  
    private Transform player;  

    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene. Please ensure the player has the tag 'Player'.");
            }
        }
    }
    
    void Update()
    {
        // Align Y position with the player
        if (!boomerang.inContact & !boomerang.attacking)
        {
            Vector3 pos = transform.position;
            float targetY = player.position.y;
            pos.y = Mathf.MoveTowards(pos.y, targetY, boomerang.getAlignSpeed() * Time.deltaTime);
            transform.position = pos;
        }

        if (boomerang != null)
        {
            if (boomerang.dead)
            {
                Destroy(gameObject); 
            }
        }
    }
}

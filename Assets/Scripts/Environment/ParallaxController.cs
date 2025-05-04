using Player;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    public bool continueParallax = true;

    // Update is called once per frame
    void Update()
    {
        if (player.linearVelocityX > 0)
        {
            continueParallax = true;
        }
        else 
        {
            continueParallax = false;
        }
    }
}

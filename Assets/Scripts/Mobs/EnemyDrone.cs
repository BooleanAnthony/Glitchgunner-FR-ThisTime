using Environment;
using Player;
using UnityEngine;

public abstract class EnemyDrone : MonoBehaviour
{
    [Header("Base Stats")]
    public float health;
    public float alignSpeed;
    public float firingSpeed;
    public int damage = 1;  
    public float difficulty = 1; //multiplier for HP/Damage

    //For handling death
    protected bool deathTriggered = false;
    protected new BoxCollider2D collider;
    public Animator anim; 

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
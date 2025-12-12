using System.Threading;
using Environment;
using Player;
using UnityEngine;

public class DirectionalBullet : EnemyBullet
{
    public float rotation;
    private Vector2 _direction;
    public Vector2 spawnPoint;
    

    void Start()
    {
        _age = 0f;
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        _age += Time.deltaTime;
            
            if (_age >= lifetime)
                Destroy(gameObject);
        
        transform.position = Movement(_age);
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }
}

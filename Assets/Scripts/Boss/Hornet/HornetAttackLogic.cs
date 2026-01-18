using System.Collections;
using BossFights;
using UnityEngine;

public class HornetAttackLogic : MonoBehaviour
{
    public enum AttackMode
    {
        Idle,
        SpawnModeDasher,
        SpawnModeShooter,
        AttackMode
    }
    public AttackMode currentAttackMode;
    public MiniBoss miniBoss;
    public float timerBetweenModeChange = 10f;
    [Header("Initial Movement")]
    public Vector2 initialTargetPosition;
    public float speed = 5f;
    [Header("AttackMode")]
    public BulletAttack bulletAttack;
    [Header("SpawnMode")]
    public HornetEnemySpawner dasherEnemySpawner;
    public HornetEnemySpawner shooterEnemySpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllScripts();
        miniBoss = GetComponent<MiniBoss>();
        StartCoroutine(ModeChange());
    }

    IEnumerator ModeChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerBetweenModeChange);

            print("Mode Change");
            if (currentAttackMode == AttackMode.Idle)
            {
                timerBetweenModeChange *= 2;
            }
            currentAttackMode = (AttackMode)Random.Range((int)AttackMode.SpawnModeDasher, (int)AttackMode.AttackMode + 1);
            DisableAllScripts();
            if (currentAttackMode == AttackMode.AttackMode)
            {
                bulletAttack.enabled = true;
                bulletAttack.RandomizeBulletType();
            }
            if (currentAttackMode == AttackMode.SpawnModeDasher)
            {
                dasherEnemySpawner.enabled = true;
                dasherEnemySpawner.isActive = true;
                dasherEnemySpawner.DecideOnEnemy();
            }
            if (currentAttackMode == AttackMode.SpawnModeShooter)
            {
                shooterEnemySpawner.enabled = true;
                shooterEnemySpawner.isActive = true;
                shooterEnemySpawner.DecideOnEnemy();
            }
        }
    }

    void DisableAllScripts()
    {
        bulletAttack.enabled = false;
        dasherEnemySpawner.enabled = false;
        dasherEnemySpawner.isActive = false;
        shooterEnemySpawner.enabled = false;
        shooterEnemySpawner.isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAttackMode == AttackMode.Idle)
        {
            Vector2 newPos = new(initialTargetPosition.x, initialTargetPosition.y);
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        }
    }
}

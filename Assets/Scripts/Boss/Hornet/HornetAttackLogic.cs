using System.Collections;
using BossFights;
using UnityEngine;

public class HornetAttackLogic : MonoBehaviour
{
    public enum AttackMode
    {
        Idle,
        SpawnMode,
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
    public HornetEnemySpawner hornetEnemySpawner;
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
            currentAttackMode = (AttackMode)Random.Range((int)AttackMode.SpawnMode, (int)AttackMode.AttackMode + 1);
            DisableAllScripts();
            if (currentAttackMode == AttackMode.AttackMode)
            {
                bulletAttack.enabled = true;
                bulletAttack.RandomizeBulletType();
            }
            if (currentAttackMode == AttackMode.SpawnMode)
            {
                hornetEnemySpawner.enabled = true;
                hornetEnemySpawner.isActive = true;
            }
        }
    }

    void DisableAllScripts()
    {
        bulletAttack.enabled = false;
        hornetEnemySpawner.enabled = false;
        hornetEnemySpawner.isActive = false;
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

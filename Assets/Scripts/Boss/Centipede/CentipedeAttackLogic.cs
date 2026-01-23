using System.Collections;
using System.Text.RegularExpressions;
using BossFights;
using UnityEngine;
using UnityEngine.Rendering;

public class CentipedeAttackLogic : MonoBehaviour
{
    public enum AttackMode
    {
        Initial,
        AttackLunge,
        Idle,
        AttackLaser,
        AttackFromBack
    }
    public AttackMode currentAttackMode;
    public MiniBoss miniBoss;
    public float timerBetweenModeChange = 10f;
    [Header("Initial Movement")]
    public Vector2 initialTargetPosition;
    public float speed = 5f;
    [Header("AttackMode")]
    public GroupedBeamAttack groupedBeamAttack;
    [Header("SpawnMode")]
    public BossEnemySpawner shielderEnemySpawner;
    public BossEnemySpawner flingerEnemySpawner;
    [Header("Lunge")]
    public LungeAttack lungeAttack;
    public GroupedBeamAttack groupedBeamAttack_behind;
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
            if (currentAttackMode == AttackMode.Initial)
            {
                lungeAttack.SetOriginalPos();
                timerBetweenModeChange *= 2;
            }
            if (currentAttackMode == AttackMode.AttackLunge || currentAttackMode == AttackMode.AttackFromBack) //while lunging (which is AttackLunge or Attacking from the back post-lunge)
            {
                currentAttackMode = (AttackMode)Random.Range((int)AttackMode.Idle, (int)AttackMode.AttackFromBack + 1); // After lunging once, choose randomly between Idle, Laser, or AttackFromBack (prevents consecutive lunges)
            }
            else
            {
                currentAttackMode = (AttackMode)Random.Range((int)AttackMode.AttackLunge, (int)AttackMode.AttackLaser + 1); //can choose any form of attack, but cannot attack from the back
            }
            
            DisableAllScripts();
            if (currentAttackMode == AttackMode.AttackLunge)
            {
                StartCoroutine(lungeAttack.AttackRoutine());
            }
            else if (currentAttackMode == AttackMode.AttackFromBack)
            {
                groupedBeamAttack_behind.ActivateAllScripts(true);
            }
            else //if neither lunging or attacking from the back
            {
                StartCoroutine(lungeAttack.ReturnRoutine());
            }

            if (currentAttackMode == AttackMode.AttackLaser)
            {
                groupedBeamAttack.ActivateAllScripts(true);
            }
            if (currentAttackMode == AttackMode.Idle)
            {
                shielderEnemySpawner.enabled = true;
                shielderEnemySpawner.isActive = true;
                shielderEnemySpawner.DecideOnEnemy();
                flingerEnemySpawner.enabled = true;
                flingerEnemySpawner.isActive = true;
                flingerEnemySpawner.DecideOnEnemy();
            }
            
        }
    }

    void DisableAllScripts()
    {
        groupedBeamAttack.ActivateAllScripts(false);
        groupedBeamAttack_behind.ActivateAllScripts(false);
        shielderEnemySpawner.enabled = false;
        shielderEnemySpawner.isActive = false;
        flingerEnemySpawner.enabled = false;
        flingerEnemySpawner.isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAttackMode == AttackMode.Initial)
        {
            Vector2 newPos = new(initialTargetPosition.x, initialTargetPosition.y);
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, newPos, step);
        }
    }
}

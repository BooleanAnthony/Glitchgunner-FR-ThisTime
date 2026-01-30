using System.Collections.Generic;
using UnityEngine;

public class CentipedeBackAttack : MonoBehaviour
{
    [Header("Inactive Scripts to Activate")]
    [SerializeField] private LungeAttack lungeAttack;
    [SerializeField] private List<BulletAttack> topBulletAttack = new();
    [SerializeField] private List<BulletAttack> bottomBulletAttack = new();

    void Start()
    {
        foreach (BulletAttack script in topBulletAttack)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        foreach (BulletAttack script in bottomBulletAttack)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }

    public void ActivateAllScripts(bool value)
    {
        if (lungeAttack.targetDirection == LungeAttack.Direction.Top)
        {
            foreach (BulletAttack script in bottomBulletAttack)
            {
                if (script != null)
                {
                    script.enabled = value;
                }
            }
        }
        else
        {
            foreach (BulletAttack script in topBulletAttack)
            {
                if (script != null)
                {
                    script.enabled = value;
                }
            }
        }
    }
}

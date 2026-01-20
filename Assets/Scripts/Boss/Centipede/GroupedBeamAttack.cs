using System;
using System.Collections.Generic;
using UnityEngine;

public class GroupedBeamAttack : MonoBehaviour
{

    [Header("Inactive Scripts to Activate")]
    [SerializeField] private List<BulletAttack> scriptsToActivate = new();

    void Start()
    {
        foreach (BulletAttack script in scriptsToActivate)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }

    public void ActivateAllScripts(bool value)
    {
        foreach (BulletAttack script in scriptsToActivate)
        {
            if (script != null)
            {
                script.enabled = value;
            }
        }
    }
}

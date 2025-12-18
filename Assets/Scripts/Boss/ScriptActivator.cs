using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    [Serializable]
    public class Attack //for bosses with multiple attacks per stage
    {
        public MonoBehaviour attackScript;
        public int stage;
        public bool CheckStage(int check)
        {
            return check >= stage;
        }
        public void ToggleScript(bool value)
        {
            attackScript.enabled = value;
        }
    }

    [Header("Inactive Scripts to Activate")]
    [SerializeField] private List<Attack> scriptsToActivate = new();

    void Start()
    {
        foreach (Attack script in scriptsToActivate)
        {
            if (script.attackScript != null)
            {
                script.ToggleScript(false);
            }
        }
    }

    public void ActivateAllScripts(int stage)
    {
        foreach (Attack script in scriptsToActivate)
        {
            if (script.attackScript != null && script.CheckStage(stage))
            {
                script.ToggleScript(true);
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{
    [Header("Inactive Scripts to Activate")]
    [SerializeField] private List<MonoBehaviour> scriptsToActivate = new List<MonoBehaviour>();

    public void ActivateAllScripts()
    {
        foreach (MonoBehaviour script in scriptsToActivate)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }
}

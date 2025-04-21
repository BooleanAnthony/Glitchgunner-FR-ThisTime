using System.Collections;
using UnityEngine;

public class DelayedBossSpawn : MonoBehaviour
{
    [SerializeField] private GameObject miniBoss;
    void Start()
    {
        StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(5f);
        miniBoss.SetActive(true);
    }
}

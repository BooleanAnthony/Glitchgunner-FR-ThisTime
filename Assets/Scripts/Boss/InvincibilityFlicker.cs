using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvincibilityFlicker : MonoBehaviour
{
    [SerializeField] private float flickerDuration = 5f;
    [SerializeField] private float flickerInterval = 0.1f;

    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        // Get all SpriteRenderers in this GameObject and its children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartFlicker()
    {
        StopAllCoroutines();
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < flickerDuration)
        {
            float alpha = visible ? 0.2f : 1f;
            SetAlpha(alpha);

            visible = !visible;
            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        // Reset alpha to fully visible at the end
        SetAlpha(1f);
    }

    private void SetAlpha(float alpha)
    {
        foreach (var sr in spriteRenderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }
}

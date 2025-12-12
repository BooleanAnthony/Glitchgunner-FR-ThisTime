using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;
    [SerializeField] private Image healthbarOutline;

    void Start()
    {
        totalHealthbar.fillAmount = GameManager.instance.playerHealth / 10;
        currentHealthbar.fillAmount = GameManager.instance.baseHealth / 10;
        healthbarOutline.fillAmount = GameManager.instance.playerHealth / 10;

        GameManager.instance.playerHealth = GameManager.instance.baseHealth;
    }

    void Update()
    {
        currentHealthbar.fillAmount = GameManager.instance.playerHealth / 10;

        if (GameManager.instance.justhealed)
        {
            StartCoroutine(ShowOutline());
        }
    }

    private IEnumerator ShowOutline()
    {
        healthbarOutline.enabled = true;
        yield return new WaitForSeconds(3f);
        healthbarOutline.enabled = false;
        GameManager.instance.justhealed = false;
    }
}

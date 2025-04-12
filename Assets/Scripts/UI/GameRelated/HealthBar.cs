using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    void Start()
    {
        totalHealthbar.fillAmount = GameManager.instance.playerHealth / 10;
    }

    void Update()
    {
        currentHealthbar.fillAmount = GameManager.instance.playerHealth / 10;
    }
}

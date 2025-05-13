using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerChar"))
        {
            GameManager.instance.HealPlayer(healthValue);
            gameObject.SetActive(false);

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddToScore(Mathf.RoundToInt(healthValue) * 5);
            }
        }
    }
}

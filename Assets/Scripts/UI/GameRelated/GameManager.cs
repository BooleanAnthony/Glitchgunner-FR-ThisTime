using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float playerHealth = 10f; // Default health

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

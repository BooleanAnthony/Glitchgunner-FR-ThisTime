using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI highscoreText;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene is loaded, try to find the HIGHSCORE text
        GameObject go = GameObject.Find("HIGHSCORE");
        if (go != null)
        {
            Debug.Log("Found");
            highscoreText = go.GetComponent<TextMeshProUGUI>();
            if (highscoreText != null)
            {
                highscoreText.text = "HIGH-SCORE: " + score;
            }
        }
    }

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            if (ScoreManager.instance.GetScore() > score)
            {
                Debug.Log("Found");
                score = ScoreManager.instance.GetScore();
            }
        }
    }
}

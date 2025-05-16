using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject additionalScoreTextBox;
    [SerializeField] private int baseScore;
    [SerializeField] private float timeLimit;
    public int displayScore;
    private int score;
    public static ScoreManager instance;
    private float timer = 0;
    private bool textActive = false;

    public int GetScore()
    {
        return score;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        displayScore = baseScore;
        score = baseScore;
    }

    // Update is called once per frame
    void Update()
    {
        additionalScoreTextBox.SetActive(textActive);
        scoreText.text = "SCORE: " + score; //DO NOT USE LOWERCASE UNLESS YOU CHANGED THE FONT ASSET

        if (textActive)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                textActive = false;
                timer = 0f;
            }
        }
    }

    public void AddToScore(int value)
    {
        score += value;
        textActive = true;
        TextMeshProUGUI additionalScoreText = additionalScoreTextBox.GetComponent<TextMeshProUGUI>();

        if (value > 0)
        {
            additionalScoreText.text = "+" + value;
            additionalScoreText.color = Color.green;
        }
        else 
        {
            additionalScoreText.text = value.ToString();
            additionalScoreText.color = Color.red;
        }
        
        if (score < 0)
        {
            score = 0;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu") // Replace with your actual scene name
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        instance = null;
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;
    private static string nextScene = "Testing Place";

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);

        if (nextScene == "Actual Game")
        {
            nextScene = "Testing Place";
        }

        if (nextScene == "Testing Place")
        {
            nextScene = "Actual Game";
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    public Button cancelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cancelButton.onClick.AddListener(MenuLoad);
    }

    private void MenuLoad()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

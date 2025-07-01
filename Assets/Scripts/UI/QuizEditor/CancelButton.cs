using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    public AudioSource audioPlayer;
    public Button cancelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cancelButton.onClick.AddListener(MenuLoad);
    }

    private void MenuLoad()
    {
        audioPlayer.Play();
        SceneManager.LoadScene("Main Menu");
    }
}

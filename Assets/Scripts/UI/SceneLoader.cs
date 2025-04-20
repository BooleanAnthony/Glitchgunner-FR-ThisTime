using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button cancelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cancelButton.onClick.AddListener(MenuLoad);
    }

    // Update is called once per frame - k
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetKeyDown(KeyCode.E)) {
            SceneLoad();
        }

        if (Input.GetKeyDown(KeyCode.E))  //Temporarily opens Editor Mode - k
        {
            EditorLoad();
        }
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene("Actual Game"); //loads the game scene
    }

    private void EditorLoad() //Sceneloader but for Editor Mode - k
    {
        SceneManager.LoadScene("Editor Mode"); //loads Editor Mode - k
    }

    private void MenuLoad()
    {
        SceneManager.LoadScene("Main Menu"); //loads MainMenu (Could be used later to exit the game) - k
    }
}

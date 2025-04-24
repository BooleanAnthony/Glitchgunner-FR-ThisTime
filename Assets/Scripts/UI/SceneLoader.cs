using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button cancelButton;
    private bool isEditorOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cancelButton.onClick.AddListener(MenuLoad);
    }

    // Update is called once per frame - k
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetKeyDown(KeyCode.E) && !isEditorOpen) {
            SceneLoad();
        }

        if (Input.GetKeyDown(KeyCode.E) && isEditorOpen == false)  //Temporarily opens Editor Mode - k
        {
            print(isEditorOpen);
            isEditorOpen = true;
            print(isEditorOpen);
            EditorLoad();
        }

        Debug.Log(isEditorOpen);
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

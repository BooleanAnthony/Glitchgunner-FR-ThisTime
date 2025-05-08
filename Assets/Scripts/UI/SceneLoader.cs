using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool isEditorOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame - k
    void LateUpdate()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetKeyDown(KeyCode.E) && !isEditorOpen) {
            SceneLoad();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isEditorOpen)  //Temporarily opens Editor Mode - k
        {
            isEditorOpen = true;
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
}

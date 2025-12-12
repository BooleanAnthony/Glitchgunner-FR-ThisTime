using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool isEditorOpen = false;
    public AudioSource audioPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame - k
    void LateUpdate()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetKeyDown(KeyCode.E) && !isEditorOpen) {
            audioPlayer.Play();
            SceneLoad();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isEditorOpen)  //Temporarily opens Editor Mode - k
        {
            audioPlayer.Play();
            isEditorOpen = true;
            EditorLoad();
        }
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene("Actual Game"); //loads the game scene
        audioPlayer.Play();
    }

    private void EditorLoad() //Sceneloader but for Editor Mode - k
    {
        SceneManager.LoadScene("ChoiceSelect"); //loads entrance to editor mode - m
        audioPlayer.Play();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0)) {
            SceneLoad();
        }
    }

    private void SceneLoad(){
        SceneManager.LoadScene("Actual Game"); //loads the game scene
    }
}

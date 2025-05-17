using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] string nextScene;
    public void SceneLoad(){
        SceneManager.LoadScene(nextScene); //loads the game scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerChar")) {
			SceneLoad();
		}
    }
}

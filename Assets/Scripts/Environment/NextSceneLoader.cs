using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] string nextScene;
    private void SceneLoad(){
        SceneManager.LoadScene(nextScene); //loads the game scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision detected");
        if (collision.gameObject.CompareTag("PlayerChar")) {
            print("Its a player!");
			SceneLoad();
		}
    }
}

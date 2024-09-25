using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошел в зону для загрузки новой сцены");
            SceneManager.LoadScene("CaveLevel");
            PlayerPrefs.SetInt("final", 0);
            PlayerPrefs.SetInt("NewGameStarted", 1);
        }
    }
}

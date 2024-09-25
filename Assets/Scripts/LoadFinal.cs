using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошел в зону для загрузки новой сцены");
            SceneManager.LoadScene("Final");
            PlayerPrefs.SetInt("NewGameStarted", 0);
            PlayerPrefs.SetInt("final", 1);
        }
    }
}

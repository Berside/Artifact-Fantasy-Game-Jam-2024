using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneOnEnter : MonoBehaviour
{
    public string triggerZoneTag = "next"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerZoneTag))
        {
            Debug.Log("Игрок вошел в зону для загрузки новой сцены");
            SceneManager.LoadScene("CaveLevel");
        }
    }
}

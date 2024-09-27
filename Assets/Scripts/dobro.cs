using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dobro : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject gameLIVE;
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        instructionPanel.SetActive(false);
        gameLIVE.SetActive(true);
    }
}

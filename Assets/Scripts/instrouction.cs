using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public Button startButton; 

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        instructionPanel.SetActive(false);
    }
}

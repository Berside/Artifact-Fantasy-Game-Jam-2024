using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCTRLLL : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(OnPlayClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    void OnPlayClick()
    {

    }

    void OnExitClick()
    {
        PlayerPrefs.SetInt("NewGameStarted", 0);
        SceneManager.LoadScene("MainMenu");
    }

    void OnSettingsClick()
    {
        Debug.Log("Кнопка Settings нажата");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void Update()
    {

    }
}

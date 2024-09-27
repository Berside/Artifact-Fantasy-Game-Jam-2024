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
    public GameObject pauseMenu—;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        pauseMenu—.SetActive(false);
    }


    void OnExitClick()
    {
        PlayerPrefs.SetInt("NewGameStarted", 0);
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("final", 0);
        PlayerPrefs.SetInt("NewGameStarted", 0);
    }

    void OnSettingsClick()
    {
        Debug.Log(" ÌÓÔÍ‡ Settings Ì‡Ê‡Ú‡");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void Update()
    {

    }
}

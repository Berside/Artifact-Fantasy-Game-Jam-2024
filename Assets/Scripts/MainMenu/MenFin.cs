using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenFin : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject pauseMenu�;

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
        pauseMenu�.SetActive(false);
        Cursor.visible = false;
    }


    void OnExitClick()
    {
        PlayerPrefs.SetInt("NewGameStarted", 0);
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("final", 1);
        PlayerPrefs.SetInt("NewGameStarted", 0);
    }

    void OnSettingsClick()
    {
        Debug.Log("������ Settings ������");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void Update()
    {

    }
}

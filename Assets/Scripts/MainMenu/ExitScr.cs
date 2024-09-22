using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button FAQbutton;
    public Button exitButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject FAQ;

    void Start()
    {
        // Инициализация кнопок
        playButton.onClick.AddListener(OnPlayClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        FAQbutton.onClick.AddListener(OnFAQClick);
        exitButton.onClick.AddListener(OnExitClick);
    }
    void OnPlayClick()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    void OnExitClick()
    {
        Application.Quit(); // Эта команда завершает выполнение Unity
    }

    void OnSettingsClick()
    {
        // Скрываем главное меню
        mainMenuPanel.SetActive(false);

        // Показываем панель настроек
        settingsPanel.SetActive(true);
    }
    void OnFAQClick()
    {
        mainMenuPanel.SetActive(false);
        FAQ.SetActive(true);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button exitButton;
    public Button settingsButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Start()
    {
        // Инициализация кнопок
        exitButton.onClick.AddListener(OnExitClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
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
}

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
    public Text buttonTextComponent;

    void Start()
    {
        // Инициализация кнопок
        InitializeButtons();

        // Настройка текста кнопки
        SetupButtonText();
    }

    private void InitializeButtons()
    {
        if (playButton != null && settingsButton != null && FAQbutton != null && exitButton != null)
        {
            playButton.onClick.AddListener(OnPlayClick);
            settingsButton.onClick.AddListener(OnSettingsClick);
            FAQbutton.onClick.AddListener(OnFAQClick);
            exitButton.onClick.AddListener(OnExitClick);
        }
        else
        {
            Debug.LogError("Не все кнопки инициализированы!");
        }
    }

    private void SetupButtonText()
    {
        if (buttonTextComponent != null)
        {
            if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
            {
                buttonTextComponent.text = "Продолжить";
            }
            else
            {
                buttonTextComponent.text = "Новая игра";
            }
        }
        else
        {
            Debug.LogWarning("Компонент Text не найден в объекте textBTN!");
        }
    }

    void OnPlayClick()
    {
        Debug.Log("Кнопка Play нажата");

        try
        {
            if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
            {
                SceneManager.LoadScene("CaveLevel");
                Time.timeScale = 1f;
                Cursor.visible = false;
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.visible = false;
                SceneManager.LoadScene("FirstLevel");
            }
            PlayerPrefs.DeleteKey("NewGameStarted");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при загрузке уровня: {e.Message}");
        }
    }

    void OnExitClick()
    {
        Debug.Log("Кнопка Exit нажата");
        Application.Quit(); // Эта команда завершает выполнение Unity
    }

    void OnSettingsClick()
    {
        Debug.Log("Кнопка Settings нажата");
        // Скрываем главное меню
        mainMenuPanel.SetActive(false);

        // Показываем панель настроек
        settingsPanel.SetActive(true);
    }

    void OnFAQClick()
    {
        Debug.Log("Кнопка FAQ нажата");
        mainMenuPanel.SetActive(false);
        FAQ.SetActive(true);
    }
}

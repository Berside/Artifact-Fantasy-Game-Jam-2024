using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button FAQbutton;
    public Button exitButton;
    public Button NewtButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject FAQ;
    public GameObject panel;
    public Text buttonTextComponent;

    void Start()
    {
        Cursor.visible = true;
        LoadPlayerPrefs();
        InitializeButtons();
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

        LoadPlayerPrefs(); // Ensure PlayerPrefs are loaded

        if (PlayerPrefs.GetInt("final", 0) == 1)
        {
            panel.SetActive(true);
            NewtButton.onClick.RemoveAllListeners(); // Remove any existing listeners
            NewtButton.onClick.AddListener(newClick);
        }
        else
        {
            Debug.LogWarning("�� ��� ������ ����������������!");
        }
    }


    private void SetupButtonText()
    {
        if (buttonTextComponent != null)
        {
            if (PlayerPrefs.GetInt("final", 0) == 1)
            {
                buttonTextComponent.text = "������� � ���������� ������";
            }
            else if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
            {
                buttonTextComponent.text = "����������";
            }
            else
            {
                buttonTextComponent.text = "����� ����";
            }
        }
        else
        {
            Debug.LogWarning("��������� Text �� ������ � ������� textBTN!");
        }
    }
    void OnPlayClick()
    {
        Debug.Log("������ Play ������");

        try
        {
            if (PlayerPrefs.GetInt("final", 0) == 1)
            {
                SceneManager.LoadScene("Final");
                Time.timeScale = 1f;
                Cursor.visible = false;
            }
            else if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
            {
                SceneManager.LoadScene("CaveLevel");
                Time.timeScale = 1f;
                Cursor.visible = false;
            }
            else
            {
                SceneManager.LoadScene("FirstLevel");
                Time.timeScale = 1f;
            }
            PlayerPrefs.DeleteKey("NewGameStarted");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������ ��� �������� ������: {e.Message}");
        }
    }
    void newClick()
    {
        SceneManager.LoadScene("FirstLevel");
        Time.timeScale = 1f;
    }
    private void LoadPlayerPrefs()
    {
        PlayerPrefs.GetInt("final", 0);
        PlayerPrefs.GetInt("NewGameStarted", 0);
        PlayerPrefs.Save(); // Save PlayerPrefs after loading
    }


    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("final", PlayerPrefs.GetInt("final", 0));
        PlayerPrefs.SetInt("NewGameStarted", PlayerPrefs.GetInt("NewGameStarted", 0));
        PlayerPrefs.Save();
    }

    void OnExitClick()
    {
        Debug.Log("������ Exit ������");
        SavePlayerPrefs();
        Application.Quit(); // ��� ������� ��������� ���������� Unity
    }

    void OnSettingsClick()
    {
        Debug.Log("������ Settings ������");
        // �������� ������� ����
        mainMenuPanel.SetActive(false);

        // ���������� ������ ��������
        settingsPanel.SetActive(true);
    }

    void OnFAQClick()
    {
        Debug.Log("������ FAQ ������");
        mainMenuPanel.SetActive(false);
        FAQ.SetActive(true);
    }
}

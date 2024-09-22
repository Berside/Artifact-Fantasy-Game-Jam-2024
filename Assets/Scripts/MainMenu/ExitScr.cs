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
        // ������������� ������
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
        Application.Quit(); // ��� ������� ��������� ���������� Unity
    }

    void OnSettingsClick()
    {
        // �������� ������� ����
        mainMenuPanel.SetActive(false);

        // ���������� ������ ��������
        settingsPanel.SetActive(true);
    }
    void OnFAQClick()
    {
        mainMenuPanel.SetActive(false);
        FAQ.SetActive(true);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button exitButton;
    public Button settingsButton;
    public Button FAQbutton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject FAQ;

    void Start()
    {
        // ������������� ������
        exitButton.onClick.AddListener(OnExitClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        FAQbutton.onClick.AddListener(OnFAQClick);
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

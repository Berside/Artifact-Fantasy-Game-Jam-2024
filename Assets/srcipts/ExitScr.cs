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
        // ������������� ������
        exitButton.onClick.AddListener(OnExitClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
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
}

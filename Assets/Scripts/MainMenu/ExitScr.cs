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
        Debug.Log("������ Play ������");
        SceneManager.LoadScene("FirstLevel");
    }

    void OnExitClick()
    {
        Debug.Log("������ Exit ������");
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

using UnityEngine;
using UnityEngine.UI;

public class cancel : MonoBehaviour
{
    public Button settingsButton;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Start()
    {
        settingsButton.onClick.AddListener(cancelClick);
    }

    void cancelClick()
    {
        // �������� ������� ����
        mainMenuPanel.SetActive(true);

        // ���������� ������ ��������
        settingsPanel.SetActive(false);
    }
}

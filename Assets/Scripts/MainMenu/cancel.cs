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
        // Скрываем главное меню
        mainMenuPanel.SetActive(true);

        // Показываем панель настроек
        settingsPanel.SetActive(false);
    }
}

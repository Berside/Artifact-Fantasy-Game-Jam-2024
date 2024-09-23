using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // Объект меню паузы
    private bool isPaused = false; // Флаг состояния паузы

    void Update()
    {
        // Проверяем нажатие клавиши ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        // Если игра не в паузе, перейдем в режим паузы
        if (!isPaused)
        {
            PauseGame();
        }
        // Если игра в паузе, возобновим игру
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем игру
        isPaused = true; // Устанавливаем флаг паузы
        pauseMenu.SetActive(true); // Активируем меню паузы
        Cursor.visible = true; // Показываем курсор
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем игру
        isPaused = false; // Устанавливаем флаг паузы
        pauseMenu.SetActive(false); // Деактивируем меню паузы
        Cursor.visible = false; // Скрываем курсор
    }
}

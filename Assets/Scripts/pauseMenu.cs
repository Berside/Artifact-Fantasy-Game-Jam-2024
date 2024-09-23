using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; 
        isPaused = true; 
        pauseMenu.SetActive(true);
        Cursor.visible = true; 
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; 
        isPaused = false; 
        pauseMenu.SetActive(false); 
        Cursor.visible = false; 
    }
}

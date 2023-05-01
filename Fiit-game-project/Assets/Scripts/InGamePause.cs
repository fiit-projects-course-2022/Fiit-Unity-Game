using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePause : MonoBehaviour
{
    public static bool IsPaused;
    public GameObject PauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        IsPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        IsPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void InMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}

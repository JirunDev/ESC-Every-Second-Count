using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlSettingsMenu;
    public static bool isPaused;
    public Rigidbody player;
    public Vector3 savedVelocity = Vector3.zero;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !settingsMenu.active && !controlSettingsMenu.active)
            {
                ResumeGame();
            }
            else if(!isPaused)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        savedVelocity = player.velocity;
        player.velocity = Vector3.zero;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.velocity = savedVelocity;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        isPaused = false;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
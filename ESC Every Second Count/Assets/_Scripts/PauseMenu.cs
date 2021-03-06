using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlSettingsMenu;
    public static bool isPaused;
    public Rigidbody player;
    public Vector3 savedVelocity = Vector3.zero;
    [Header("LoadingScreen")]
    public GameObject loadingScreen;
    public Slider loadingBar;

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
        isPaused = false;
        GunItems.triggerNumber = 0;

        PlayerPrefs.SetInt("introcheck", 1);

        //LoadingScreen
        StartCoroutine(LoadSceneAsynchronously(0));
    }

    public void QuitToDesktop()
    {
        PlayerPrefs.SetInt("introcheck", 0);
        Application.Quit();
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
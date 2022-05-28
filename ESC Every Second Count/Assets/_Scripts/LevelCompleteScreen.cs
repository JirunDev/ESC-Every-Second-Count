using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelCompleteScreen : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    [Header("Text Stuffs")]
    public TextMeshProUGUI totalTime;
    public TextMeshProUGUI possessGun;
    public TextMeshProUGUI possessGunScore;
    public TextMeshProUGUI exGunTimer;
    public TextMeshProUGUI exGunTimerScore;
    public TextMeshProUGUI exTimeCons;
    public TextMeshProUGUI exTimeConsScore;
    public TextMeshProUGUI totalScore;
    [Header("Game Objects")]
    public Rigidbody player;
    public GameObject loadingScreen;
    public Slider loadingBar;
    [Header("Music")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    private int score;
    private float time;

    public void LevelComplete()
    {
        levelCompleteScreen.SetActive(true);

        //music
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.Play();

        //total time
        time = Time.timeSinceLevelLoad;
        totalTime.text = "Total Time: " + $"{time / 60:00}:{time % 60:00}:{(time * 1000) % 1000:000}";

        //score calculations
        totalScore.text = CalculateScore().ToString();
        
        //stop everything at the end
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.velocity = Vector3.zero;
    }

    private int CalculateScore()
    {
        score = 1500;
        if (GetComponent<GunTimer>().pickGun.isOn)
        {
            score += 500;
            possessGunScore.text = "+500";
            score += GunTimer.remainingDuration * 10;
            exGunTimerScore.text = $"+{GunTimer.remainingDuration * 10}";
        }
        else
        {
            possessGun.color = Color.red;
            possessGunScore.color = Color.red;
            exGunTimer.color = Color.red;
            exGunTimerScore.color = Color.red;
        }

        if ((((int)time) - PortalController.countDown - 120) > 0)
        {
            score -= (((int)time) - PortalController.countDown - 120);
            exTimeConsScore.text = $"-{(((int)time) - PortalController.countDown - 120)}";
        }
        else
        {
            exTimeCons.color = Color.white;
            exTimeConsScore.color = Color.white;
        }

        return score;
    }

    public void ContinueNextLevel(int levelIndex)
    {
        //Time.timeScale = 1f;
        GunItems.triggerNumber = 0;

        //LoadingScreen
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
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

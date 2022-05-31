using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public GameObject intro;
    public GameObject mainMenu;
    public VideoPlayer video;

    private int introCheck;

    void Start()
    {
        //recover value
        introCheck = PlayerPrefs.GetInt("introcheck",0);

        if (introCheck != 0)
        {
            intro.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            video.loopPointReached += EndReached;
        }
    }

    public void EndReached(VideoPlayer vp)
    {
        intro.SetActive(false);
        mainMenu.SetActive(true);
    }
}

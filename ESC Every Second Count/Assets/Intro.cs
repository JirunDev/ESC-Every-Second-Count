using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject intro;
    public GameObject mainMenu;

    private int introCheck;

    void Start()
    {
        //recover value
        introCheck = PlayerPrefs.GetInt("introcheck",0);
    }

    // Update is called once per frame
    void Update()
    {
        if(introCheck != 0)
        {
            intro.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}

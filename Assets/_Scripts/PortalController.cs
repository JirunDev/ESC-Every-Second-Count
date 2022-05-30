using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortalController : MonoBehaviour
{
    public GameObject portal;
    public GameObject portalBlacker;
    public TextMeshPro timer;
    public GameObject pressE;
    public Toggle activToggle;
    public AudioClip pingPong;

    private bool isBooting = false;
    public static int countDown;
    private int remainingDuration;
    private bool usedOncePerMap = true;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        countDown = Random.Range(210,241);
    }

    void Update()
    {
        if (isBooting && usedOncePerMap)
        {
            portal.SetActive(true);
            Being(countDown);
            isBooting = false;
            usedOncePerMap = false;
            activToggle.isOn = true;
            audioSource.PlayOneShot(pingPong);
        }
    }
    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            timer.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }
    private void OnEnd()
    {
        //End Time , if want Do something
        timer.color = Color.green;
        portalBlacker.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player" && usedOncePerMap)
        {
            pressE.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            pressE.SetActive(false);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Player")
        {
            isBooting = true;
            pressE.SetActive(false);
        }
    }
}

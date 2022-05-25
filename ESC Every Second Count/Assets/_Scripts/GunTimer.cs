using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GunTimer : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject cdText;
    public Toggle pickGun;
    public static int Duration = 0;
    public static int remainingDuration;
    private bool Pause;
    private bool checkStartTimer = false;

    public AudioSource audioSource;
    public AudioClip pickUpSound;

    private void FixedUpdate()
    {
        if (checkStartTimer == false) Duration = GunItems.countdownValue;
        if (Duration != 0 && checkStartTimer == false)
        {
            uiText.color = Color.white;
            checkStartTimer = true;

            audioSource.PlayOneShot(pickUpSound);
            pickGun.isOn = true;
            cdText.SetActive(true);

            Being(Duration);
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
            if (!Pause)
            {
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        //End Time , if want Do something
        uiText.color = Color.red;
        Duration = 0;
        checkStartTimer = false;

        pickGun.isOn = false;
        cdText.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public Toggle toggle3;
    public Toggle toggle4;
    public LevelCompleteScreen lcsScript;
    public GameObject killBossPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (toggle3.isOn)
        {
            toggle4.isOn = true;

            lcsScript.GetComponent<LevelCompleteScreen>().LevelComplete();
        }
        else
        {
            StartCoroutine(KillBossPrompt());
        }
    }

    IEnumerator KillBossPrompt()
    {
        killBossPrompt.SetActive(true);
        yield return new WaitForSeconds(5);
        killBossPrompt.SetActive(false);
    }
}

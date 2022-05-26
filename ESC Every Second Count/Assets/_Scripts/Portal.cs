using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public Toggle toggle;
    public LevelCompleteScreen lcsScript;

    private void OnTriggerEnter(Collider other)
    {
        toggle.isOn = true;

        lcsScript.GetComponent<LevelCompleteScreen>().LevelComplete();
    }
}

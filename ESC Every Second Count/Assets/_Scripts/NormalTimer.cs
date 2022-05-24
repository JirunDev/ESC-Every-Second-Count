using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NormalTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    private float time = 0;

    void Update()
    {
        time = Time.timeSinceLevelLoad;
        uiText.text = $"{time / 60:00}:{time % 60:00}:{(time * 1000) % 1000:000}";
    }
}

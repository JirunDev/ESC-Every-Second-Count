using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ambiance : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource aS;
    public AudioClip aC;

    private void Awake()
    {
        aS.loop = true;
        aS.clip = aC;
        aS.Play();
    }

    private void Update()
    {
        if (toggle.isOn)
        {
            aS.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip genshinSound;  
    public AudioClip actualSound;
    void Start()
    {
        audioSource.loop = true;

        if (transform.name == "FakeOpening") audioSource.clip = genshinSound;
        if (transform.name == "MainMenu") audioSource.clip = actualSound;

        audioSource.Play();
    }
}

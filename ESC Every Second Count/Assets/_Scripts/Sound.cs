using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("Setup")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sounds")]
    public AudioClip damageSound;

    public void PlaySFXSound(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayDamageSound()
    {
        sfxSource.PlayOneShot(damageSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSFX : MonoBehaviour
{
    [SerializeField] AudioClip[] walkingAudioClips;
    [SerializeField] AudioClip[] jumpingAudioClips;

    private AudioSource SFXAudioSource;

    private int lastIndex = 0;

    private void Awake()
    {
        SFXAudioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        AudioClip clip = GetRandomWalkClip();
        SFXAudioSource.PlayOneShot(clip);
    }
    private AudioClip GetRandomWalkClip()
    {
        int index = Random.Range(0, walkingAudioClips.Length);
        if (lastIndex != index)
        {
            lastIndex = index;
            return walkingAudioClips[index];
        }
        else if (lastIndex == index && index == walkingAudioClips.Length-1)
        {
            lastIndex -= 1;
            return walkingAudioClips[index - 1];
        }
        else
        {
            lastIndex += 1;
            return walkingAudioClips[index + 1];
        }
    }

    public void Jump()
    {
        AudioClip clip = GetRandomJumpClip();
        SFXAudioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomJumpClip()
    {
        int index = Random.Range(0, jumpingAudioClips.Length);
        return jumpingAudioClips[index];
    }
}

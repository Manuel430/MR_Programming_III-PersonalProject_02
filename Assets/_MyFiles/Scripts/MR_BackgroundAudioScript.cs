using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_BackgroundAudioScript : MonoBehaviour
{
    [SerializeField] AudioClip stealthAudio;
    [SerializeField] AudioClip hunterAudio;

    [SerializeField] AudioSource audioSource;

    GameObject hunterArrival;

    public void NormalBackground()
    {
        audioSource.clip = stealthAudio;
        audioSource.Play();
    }

    public void HunterBackground()
    {
        audioSource.clip = hunterAudio;
        audioSource.Play();
    }

}

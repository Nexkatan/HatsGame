using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SFXClips : MonoBehaviour
{
    public AudioClip[] rotateClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomRotateClip()
    {
        audioSource.clip = rotateClips[UnityEngine.Random.Range(0, rotateClips.Length)];
        audioSource.Play();
    }


}

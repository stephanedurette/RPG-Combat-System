using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;

    public float Volume { get { return audioSource.volume; } set { audioSource.volume = value; } }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}

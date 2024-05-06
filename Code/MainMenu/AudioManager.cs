using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static float savedVolume = 0.8f; // Static variable to store the volume value

    public AudioSource audioSource; // Reference to the AudioSource component

    void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
    }

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Set the volume of the audio source
        audioSource.volume = savedVolume;
        audioSource.Play();
    }

    public void ChangeVolume(float volume)
    {
        // Set the volume of the audio source
        audioSource.volume = volume;
        savedVolume = volume; // Update the static variable
    }
}

// Handles Audio
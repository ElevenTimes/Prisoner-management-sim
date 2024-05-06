using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private AudioManager audioManager;

    void Start()
    {
        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();
        
        // Get the Slider component attached to this GameObject
        volumeSlider = GetComponent<Slider>();

        // Set the initial value of the slider to the saved volume
        volumeSlider.value = AudioManager.savedVolume;

        // Add a listener for when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float volume)
    {
        // Change the volume in the AudioManager
        audioManager.ChangeVolume(volume);
    }
}

// Script responsible for making audio slider work


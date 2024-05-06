using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Shown when user pauses the game
public class EscapeMenu : MonoBehaviour
{
    public GameObject escapeMenuPanel; // Reference to the escape menu UI panel
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check for Escape key and UI visibility
        {
            ToggleEscapeMenu(); // Call a function to toggle the escape menu visibility
        }
    }
    void ToggleEscapeMenu()
    {
        // Activate or deactivate the escape menu UI panel based on its current state
        escapeMenuPanel.SetActive(!escapeMenuPanel.activeSelf);
    }

    public void Resume() {
        ToggleEscapeMenu(); // Call a function to toggle the escape menu visibility
    }

    public void MainMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit() {
        Application.Quit();
    }
}

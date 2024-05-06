using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement;
using TMPro;

// Shown whenever player either loses or wins the game
public class EndScreen : MonoBehaviour
{
    public GameObject endScreenPanel; // Reference to the End screen UI panel
    public TMP_Text loseText;
    public TMP_Text winText;

    void Start() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
    }
    public void GameLost() {
        ToggleEndScreen();
        loseText.gameObject.SetActive(true);

    }

    public void GameWon() {
        ToggleEndScreen();
        winText.gameObject.SetActive(true);

    }
    void ToggleEndScreen()
    {
        // Deactivate the End screen UI panel
        endScreenPanel.SetActive(!endScreenPanel.activeSelf);
    }

    public void MainMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit() {
        Application.Quit();
    }
}


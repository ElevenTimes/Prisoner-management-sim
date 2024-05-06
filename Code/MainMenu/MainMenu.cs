using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField accountNameInputField;
    public TMP_InputField passwordInputField;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    // When user presses the Play button the application checks if it follows the guidelines. If it does then user is sent to next scene
    public void Play() {

        string accountName = accountNameInputField.text;
        string password = passwordInputField.text;

        if (accountName.Length >= 5)
        {
            string account = accountName;

            Debug.Log("Your account is: " + account);

            PlayerPrefs.SetString("Account", account);
            PlayerPrefs.Save(); // Save the PlayerPrefs data immediately

            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Debug.Log("Enter at least 5 characters.");
        }
    
    }

    // Function that let's user delete their saved data
    public void RestartGame()
    {
        string accountName = accountNameInputField.text;
        string password = passwordInputField.text;

        if (!string.IsNullOrEmpty(accountName))
        {
            string account = accountName;
            string filePath = Path.Combine(Application.persistentDataPath, account + ".json");

            // Check if the JSON file exists
            if (File.Exists(filePath))
            {
                // Delete the JSON file associated with the player's account
                File.Delete(filePath);
                Debug.Log("Data for account " + account + " has been deleted.");
            }
            else
            {
                Debug.Log("No data found for account " + account);
            }
        }
        else
        {
            Debug.Log("Please fill in both account name and password.");
        }
    }

    // Quits game
    public void Quit() {
        Application.Quit();
    }
}

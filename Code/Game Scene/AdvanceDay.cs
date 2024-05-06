using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO; 
using System;

public class AdvanceDay : MonoBehaviour
{   
    // Declaring items public in Unity means that they'll be accessible both by different scrips and in Unity's editor itself. This way a lot of connections can be done in Unity's editor.
    public EndScreen endScreen;
    public ShowDays showDays;
    public ShowRent showRent;
    public NewPrisoners newPrisoners;
    public MovePrisoners movePrisoners;
    public CurrentPrisoners currentPrisoners;
    public CanvasScript canvasScript;
    public ShowFunds showFunds;
    public InfoBox infoBox;
    public Capacity capacity;
    public CapacityUpgrade capacityUpgrade;
    public SecurityUpgrade securityUpgrade;
    System.Random random = new System.Random();
    public Prisoner[] randomPrisoners;
    public Prisoner[] deletablePrisoners;
    private IDataService dataService = new JsonDataService();
    private int count = 0;
    private string account;
    private int chancesToEscape;

    // Does all the calcuations necessary when user presses the "Advance day" button
    public void OnMouseDown()
    {   

        movePrisoners.ResetMoveAmount();
        count += 1;
        showDays.IncrementDays();
        if (showDays.GetDays() >= 200) // Checks if user has won the game
        {
            endScreen.GameWon();
        }
        else {

            deletablePrisoners = movePrisoners.GetDeletablePrisoners();
            if (showDays.DaysAmount > 1) // A fail safe implemented so game doesn't crash on 1st day
            {
                if (deletablePrisoners == null || count >= 2) 
                {
                    deletablePrisoners = randomPrisoners;
                }
                if (deletablePrisoners != null && deletablePrisoners.Length > 0)
                {
                    newPrisoners.DestroyPrisonerButtons(deletablePrisoners);
                }
            }
            randomPrisoners = newPrisoners.AddNewPrisoners();
            movePrisoners.SetRandomPrisonersFromAdvanceDay(randomPrisoners);
            MainGameLoop(currentPrisoners.GetPrisonersInPrison()); // Calls the main loop of the game
            currentPrisoners.UpdatePrisonerButtons();
        }
        showRent.UpdateRent();
    }
    
    // Function for fail safe
    public void ResetCount() {
        count = 0;
    }

    // Function that either creates a new account or logs into an existing one if account info matches.
    private void Start()
    {   
        account = canvasScript.GetAccount();
        string filePath = "/" + account + ".json";
        Debug.Log("Account retrieved from CanvasScript: " + filePath);

        if (!File.Exists(filePath))
        {   
            dataService.CreateNewFile(filePath, "[]");
        }
        LoadPrisonersFromJsonAndCreateButtons();

        if (showDays.GetDays() >= 200) 
        {
            endScreen.GameWon();
        }
        else if (showFunds.GetFunds() < 0 && showRent.GetDaysLeft() == 10)
        {
            endScreen.GameLost();
        }
        
        
    }

    // Function for loading user data from Json file
    private void LoadPrisonersFromJsonAndCreateButtons()
    {
    // Specify the file path
        account = canvasScript.GetAccount();
        string filePath = "/" + account + ".json";
        Debug.Log("Account retrieved from CanvasScript: " + filePath);

        // Load data from JSON file
        Dictionary<string, string> jsonData = dataService.LoadData<Dictionary<string, string>>(filePath);

        if (jsonData != null)
        {
            // Deserialize prisoner data
            if (jsonData.ContainsKey("prisoners"))
            {
                string prisonerData = jsonData["prisoners"];
                Prisoner[] loadedPrisoners = JsonConvert.DeserializeObject<Prisoner[]>(prisonerData);

                if (loadedPrisoners != null)
                {
                    currentPrisoners.AddLoadedPrisoners(loadedPrisoners);
                }
            }

            // Deserialize funds data (assuming funds are stored as an int)
            if (jsonData.ContainsKey("funds"))
            {
                string fundsData = jsonData["funds"];
                int loadedFunds = JsonConvert.DeserializeObject<int>(fundsData);
                showFunds.SetFunds(loadedFunds);
            }

            if (jsonData.ContainsKey("rent"))
            {
                string rentData = jsonData["rent"];
                int loadedRent = JsonConvert.DeserializeObject<int>(rentData);
                showRent.SetRent(loadedRent);
            }

            if (jsonData.ContainsKey("daysLeft"))
            {
                string daysLeftData = jsonData["daysLeft"];
                int loadedDaysLeft = JsonConvert.DeserializeObject<int>(daysLeftData);
                showRent.SetDaysLeft(loadedDaysLeft);
            }

            if (jsonData.ContainsKey("days"))
            {
                string daysData = jsonData["days"];
                int loadedDays = JsonConvert.DeserializeObject<int>(daysData);
                showDays.SetDays(loadedDays);
            }

            if (jsonData.ContainsKey("securityHigh"))
            {
                string securityHigh = jsonData["securityHigh"];
                int loadedSecurityHigh = JsonConvert.DeserializeObject<int>(securityHigh);
                securityUpgrade.SetSecurityHigh(loadedSecurityHigh);
                securityUpgrade.SetSecurityLow(securityUpgrade.GetSecurityHigh() / 100);
            }
            if (jsonData.ContainsKey("upgradeCost"))
            {
                string upgradeCost = jsonData["upgradeCost"];
                int loadedUpgradeCost = JsonConvert.DeserializeObject<int>(upgradeCost);
                securityUpgrade.SetUpgradeCost(loadedUpgradeCost);
                securityUpgrade.SetUpgradeCostText();
            }

            if (jsonData.ContainsKey("capacity"))
            {
                string capacityData = jsonData["capacity"];
                int loadedCapacity = JsonConvert.DeserializeObject<int>(capacityData);
                capacity.SetCapacity(loadedCapacity);
            }
            if (jsonData.ContainsKey("upgradeCost2"))
            {
                string upgradeCost = jsonData["upgradeCost2"];
                int loadedUpgradeCost = JsonConvert.DeserializeObject<int>(upgradeCost);
                capacity.SetUpgradeCost(loadedUpgradeCost);
                capacityUpgrade.SetUpgradeCostText();
                capacity.SetCapacityText();
            }
        }
        else
        {
            Debug.LogError("Failed to load prisoner or funds data");
        }
    }


    // The main loop of the game. Here the game calculates all prisoner sentences, if any prisoner has escaped and user's funds
    public void MainGameLoop(List<Prisoner> prisoners)
    {
        List<Prisoner> prisonersToRemove = new List<Prisoner>();
        List<Prisoner> escapedPrisoners = new List<Prisoner>();

        foreach (Prisoner prisoner in prisoners)
        {   
            chancesToEscape = random.Next(securityUpgrade.securityLow, securityUpgrade.securityHigh);

            if (prisoner.Sentence <= 1)
            {
                prisonersToRemove.Add(prisoner);
            }

            else if (chancesToEscape <= (int)prisoner.EscapeOdds) {

                showFunds.RemoveIncome((int)prisoner.Severity * prisoner.Sentence * 2);
                
                if (prisoner.Sentence != 1) {
                    escapedPrisoners.Add(prisoner);
                }
                prisonersToRemove.Add(prisoner);
                chancesToEscape = 0;
            }
            else {
                showFunds.AddIncome((int)prisoner.Severity);
                prisoner.ReduceSentence();
            }

        }

        // Remove prisoners and their buttons after the loop
        foreach (Prisoner prisoner in escapedPrisoners)
        {
            infoBox.UpdateInfoText("Prisoner " + prisoner.Name + " Severity: " + (int)prisoner.Severity + " has escaped! You've been charged with a " + ((int)prisoner.Severity * prisoner.Sentence * 2) + " dollar fine.");
        }
        foreach (Prisoner prisoner in prisonersToRemove)
        {
            currentPrisoners.DestroyPrisonerButtons(prisoner);
        }

        capacity.UpdatePrisonersHoused(prisoners.Count);
    }
}

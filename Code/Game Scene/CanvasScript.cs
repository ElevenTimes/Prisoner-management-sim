using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.IO; 
using System;    
public class CanvasScript : MonoBehaviour
{
    private IDataService dataService = new JsonDataService();
    public CurrentPrisoners currentPrisoners;
    public ShowFunds showFunds;
    public ShowRent showRent;
    public ShowDays showDays;
    public SecurityUpgrade securityUpgrade;
    public Capacity capacity;
    private string account;

    void Awake() {
        account = PlayerPrefs.GetString("Account", "");
    }
    public string GetAccount() {

        return account;
    }
    // Function that's responsible for saving user data
    public void SerializeJson()
    {
        List<Prisoner> prisonersInPrison = currentPrisoners.GetPrisonersInPrison();
        Prisoner[] prisonersArray = prisonersInPrison.ToArray();
        string prisonerData = JsonConvert.SerializeObject(prisonersArray);

        int fundsData = showFunds.GetFunds();
        int rentData = showRent.GetRent();
        int daysLeftData = showRent.GetDaysLeft();
        int daysData = showDays.GetDays();
        int securityHighData = securityUpgrade.GetSecurityHigh();
        int upgradeCostData = securityUpgrade.GetUpgradeCost();
        int capacityData = capacity.GetCapacity();
        int upgradeCostData2 = capacity.GetUpgradeCost();

        Dictionary<string, object> dataToSave = new Dictionary<string, object>();
        dataToSave.Add("prisoners", prisonerData);
        dataToSave.Add("funds", fundsData);
        dataToSave.Add("rent", rentData);
        dataToSave.Add("daysLeft", daysLeftData);
        dataToSave.Add("days", daysData);
        dataToSave.Add("securityHigh", securityHighData);
        dataToSave.Add("upgradeCost", upgradeCostData);
        dataToSave.Add("capacity", capacityData);
        dataToSave.Add("upgradeCost2", upgradeCostData2);

        string filePath = "/" + GetAccount() + ".json";

        if (dataService.SaveData(filePath, dataToSave))
        {
            Debug.Log("Data saved successfully");
        }
        else
        {
            Debug.LogError("Failed to save data");
        }
    }
}

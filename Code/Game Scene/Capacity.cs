using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TMPro is Unity's namespace mainly used for handling visible text. Any variable that uses .text is a text object.

public class Capacity : MonoBehaviour
{   
    public TMP_Text capacityText;
    public InfoBox infoBox;
    public ShowFunds showFunds;
    public SecurityUpgrade securityUpgrade;
    public int capacity = 5;
    public int upgradeCost = 20;
    public int prisonersHoused = 0;
    void Update()
    {   
        capacityText.text = prisonersHoused + "/" + capacity;
        
    }
    
    public int GetCapacity() {
        return capacity;
    }

    public void SetCapacity(int newCapacity) {
        capacity = newCapacity;
    }

    public int GetUpgradeCost() {
        return upgradeCost;
    }

    public void SetUpgradeCost(int newUpgradeCost) {
        upgradeCost = newUpgradeCost;
    }

    public void SetCapacityText() {
        capacityText.text = prisonersHoused + "/" + capacity;
    }

    // Function for upgrading prison's capacity. Called when user presses "Upgrade capacity" button
    public void increaseCapacity() {

        int currentFunds = showFunds.Funds;
        if (currentFunds >= upgradeCost)
        {
            showFunds.RemoveIncome(upgradeCost);
            upgradeCost = Mathf.CeilToInt(upgradeCost * 1.5f / 10) * 10;
            capacity += 5;
        }
        else {
            OnMouseDown2();
        }
    }

    public void UpdatePrisonersHoused(int newPrisonersHoused) {
        prisonersHoused = newPrisonersHoused;
    }
    public void OnMouseDown()
    {
        infoBox.UpdateInfoText("The amount of prisoners your prison is able to hold at a time. Your prison can hold " + (capacity - prisonersHoused) + " additional prisoners. Security level " + (securityUpgrade.timesUpgraded + 1) + ".");
    }

    public void OnMouseDown2()
    {
        infoBox.UpdateInfoText("You don't have enough money to upgrade the capacity of your prison. You're missing " + (upgradeCost - showFunds.Funds) + " additional funds.");
    }
}

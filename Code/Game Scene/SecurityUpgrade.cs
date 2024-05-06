using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecurityUpgrade : MonoBehaviour
{
    public TMP_Text upgradeCostText;
    public InfoBox infoBox;
    public ShowFunds showFunds;
    public Capacity capacity;
    public int securityHigh = 100;
    public int securityLow = 1;

    public int timesUpgraded = 0;
    public int upgradeCost = 30;

    private bool isFirstMessage = true;
    public int GetSecurityHigh() {
        return securityHigh;
    }

    public void SetSecurityHigh(int newSecurityHigh) {
        securityHigh = newSecurityHigh;
    }

    public int GetSecurityLow() {
        return securityLow;
    }

    public void SetSecurityLow(int newSecurityLow) {
    securityLow = newSecurityLow;
    }

    public int GetUpgradeCost() {
        return upgradeCost;
    }

    public void SetUpgradeCost(int newUpgradeCost) {
        upgradeCost = newUpgradeCost;
    }

    public void SetUpgradeCostText() {
        upgradeCostText.text = "Cost: " + upgradeCost;
    }

    // Function that's called whenever user presses the "Upgrade security". SecurityHigh is responsible for decreasing the odds of prisoner to escape. For example 10/100 is a 10% chance to escape, 10/200 however is 5% chance. SecurityLow however makes so that some prisoners aren't able to escape at all.
    public void UpgradeSecurity() {
        int currentFunds = showFunds.Funds;
        if (currentFunds >= upgradeCost)
        {   
            timesUpgraded++;
            if (timesUpgraded == 1)
            {
                securityHigh = 200;
                securityLow = 2;
            }
            else if (timesUpgraded == 2)
            {
                securityHigh = 500;
                securityLow = 5;
            }
            else if (timesUpgraded == 3)
            {
                securityHigh = 1000;
                securityLow = 10;
            }
            else if (timesUpgraded == 4)
            {
                securityHigh = 1500;
                securityLow = 15;
            }
            else if (timesUpgraded == 5)
            {
                securityHigh = 2000;
                securityLow = 20;
            }
            else if (timesUpgraded == 6)
            {
                securityHigh = 3000;
                securityLow = 30;
            }
            else if (timesUpgraded == 7)
            {
                securityHigh = 5000;
                securityLow = 50;
            }
            else if (timesUpgraded == 8)
            {
                securityHigh = 10000;
                securityLow = 100;
            }
            else if (timesUpgraded == 9)
            {
                securityHigh = 10000;
                securityLow = 200;
            }
            else if (timesUpgraded == 10)
            {
                securityHigh = 15000;
                securityLow = 250;
            }
            else if (timesUpgraded == 11)
            {
                securityHigh = 40000;
                securityLow = 400;
            }
            else if (timesUpgraded == 12)
            {
                securityHigh = 60000;
                securityLow = 600;
            }
            else if (timesUpgraded == 13)
            {
                securityHigh = 100000;
                securityLow = 1000;
            }

            Debug.Log(securityHigh);
            Debug.Log(securityLow);
            showFunds.RemoveIncome(upgradeCost);
            upgradeCost = Mathf.CeilToInt(upgradeCost * 1.6f / 10) * 10;
            upgradeCostText.text = "Cost: " + upgradeCost;
        }
        else
        {
            if (isFirstMessage)
            {
                infoBox.UpdateInfoText("You don't have enough money to upgrade the security of your prison. You're missing " + (upgradeCost - showFunds.Funds) + " additional funds.");
                isFirstMessage = false;
            }
            else
            {
                infoBox.UpdateInfoText("Upgrading security makes it harder for prisoners to escape from your prison. Upgrade enough and some prisoners won't be able to escape at all.");
                isFirstMessage = true;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// class responsible for calculating the rent user needs to pay ever day
public class ShowRent : MonoBehaviour
{
    public TMP_Text rentText;
    public ShowFunds showFunds;
    public ShowDays showDays;

    public EndScreen endScreen;
    private int rent = 10;
    private int daysLeft = 10;

    void Update()
    {
        rentText.text = "Rent of " + rent + " in " + daysLeft + " days";
    }

    public void UpdateRent()
    {   
        if (daysLeft == 1)
        {   
            showFunds.RemoveIncome(rent);
            rent = CalculateRent();
            daysLeft = 10;

            // If user doesn't have enough money when rent is due, they lose
            if (showFunds.GetFunds() < 0) {
                endScreen.GameLost();
            }

        }
        else
        {
            daysLeft--;
        }
    }

    public void SetRent(int newRent) {
        rent = newRent;
    }

    public void SetDaysLeft(int newDaysLeft) {
        daysLeft = newDaysLeft;
    }

    public int GetRent() {
        return rent;
    }

        public int GetDaysLeft() {
        return daysLeft;
    }

    // Calculates rent
    int CalculateRent()
    {
        float steepness = 0.06f; // Controls how quickly the function changes
        float midpoint = 115.0f;  // Determines where the function transitions from fast to slow
        float rentRange = 10000.0f; // Adjust to control the range of rent values

        // Calculate the rent using a sigmoid function
        float rentValue = rentRange / (1 + Mathf.Exp(-steepness * (showDays.GetDays() - midpoint)));
        float roundedRentValue = Mathf.RoundToInt(rentValue / 5) * 5;
        return Mathf.RoundToInt(roundedRentValue);
    }
}
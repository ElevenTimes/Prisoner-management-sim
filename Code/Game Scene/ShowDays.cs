using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Function for displaying the amount of days that have passsed
public class ShowDays : MonoBehaviour
{

    public TMP_Text daysAmountText;

    private int daysAmount = 0;

    public int DaysAmount
    {
        get { return daysAmount; }
    }
  
    void Update()
    {   
        daysAmountText.text = "Day " + daysAmount;
        
    }

    public void IncrementDays()
    {
        daysAmount += 1;
    }

    public int GetDays() {
        return DaysAmount;
    }

    public void SetDays(int newDaysAmount) {
        daysAmount = newDaysAmount;
    }

}

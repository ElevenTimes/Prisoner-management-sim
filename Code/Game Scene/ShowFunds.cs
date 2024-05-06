using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class that handles user's funds
public class ShowFunds : MonoBehaviour
{

    public TMP_Text fundsText;

    private int funds = 0;

    public int Funds
    {
        get { return funds; }
    }
  
    void Update()
    {   
        fundsText.text = "Funds " + funds;
        
    }

    public void AddIncome(int newFunds)
    {
        funds += newFunds;
    }

    public void RemoveIncome(int newFunds)
    {
        funds -= newFunds;
    }
    public int GetFunds() {
        return funds;
    }

    public void SetFunds(int newFunds) {
        funds = newFunds;
    }

}

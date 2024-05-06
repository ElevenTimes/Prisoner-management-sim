using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// An extension for capacity class. Would've been a better a idea to combine both of them together
public class CapacityUpgrade : MonoBehaviour
{
    public TMP_Text upgradeCostText;

    public Capacity capacity;

    public CurrentPrisoners currentPrisoners;

    public void UpgradeCapacity() {
        capacity.increaseCapacity();
        upgradeCostText.text = "Cost: " + capacity.upgradeCost;
        capacity.UpdatePrisonersHoused(currentPrisoners.GetPrisonersInPrison().Count);
    }

    public void SetUpgradeCostText() {
        capacity.UpdatePrisonersHoused(currentPrisoners.GetPrisonersInPrison().Count);
        upgradeCostText.text = "Cost: " + capacity.upgradeCost;
    }
}

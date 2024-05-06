using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrisoners : MonoBehaviour
{   

    private int moveAmount = 1;
    public Prisoner[] randomPrisoners; // Declare the randomPrisoners array here
    public NewPrisoners newPrisoners;
    public CurrentPrisoners currentPrisoners;
    private int selectedPrisonerAmount = 0;
    private Prisoner[] moveablePrisoners; // Declare moveablePrisoners as a member variable
    public Prisoner[] deletablePrisoners;

    public AdvanceDay advanceDay;
    public ShowDays showDays;
    public InfoBox infoBox;
    public Capacity capacity;

    public void SetRandomPrisonersFromAdvanceDay(Prisoner[] prisoners)
    {
    randomPrisoners = prisoners;
    }

    // Function that checks how many prisoners the user has selected
    public void CreateMoveablePrisoners(Prisoner[] prisoners)
    {   
        selectedPrisonerAmount = 0;

        foreach (Prisoner prisoner in prisoners) {
            if (prisoner.IsSelected) {
                selectedPrisonerAmount++;    
            }
        }
        moveablePrisoners = new Prisoner[selectedPrisonerAmount];

        int index = 0;
        foreach (Prisoner prisoner in prisoners) {
            if (prisoner.IsSelected) {
                moveablePrisoners[index] = prisoner;
                index++;
            }     
        }

    }

    // Function responsible for calculating which prisoners need to be removed from "Intake" list
    public Prisoner[] CreateDeletablePrisoners(Prisoner[] prisoners) 
    {
        
        selectedPrisonerAmount = 0;

        foreach (Prisoner prisoner in prisoners) {
            if (!prisoner.IsSelected) {
                selectedPrisonerAmount++;    
            }
        }
        deletablePrisoners = new Prisoner[selectedPrisonerAmount];
        int index = 0;
        foreach (Prisoner prisoner in prisoners) {
            if (!prisoner.IsSelected) {
                deletablePrisoners[index] = prisoner;
                index++;
            }     
        }
        return deletablePrisoners;
    }

    public Prisoner[] GetDeletablePrisoners()
    {
        return deletablePrisoners;
    }

    // Function that handles the moving of prisoners when "Move" button is presses
    public void OnMouseDown()
    {   
        if (showDays.GetDays() == 0)
        {
            infoBox.UpdateInfoText("There are no prisoners in intake right now. Advance to next day.");
        }
        else {
            if (moveAmount == 0) {
                infoBox.UpdateInfoText("You can only intake prisoners once per day.");
            }
            else {
                if (randomPrisoners != null) {
                    CreateMoveablePrisoners(randomPrisoners);
                    CreateDeletablePrisoners(randomPrisoners);
                }
                if (moveablePrisoners.Length <= (capacity.capacity - capacity.prisonersHoused)) {
                    
                    moveAmount -= 1;
                    advanceDay.ResetCount();
                    if (moveablePrisoners != null)
                    {   
                        newPrisoners.DestroyPrisonerButtons(moveablePrisoners);
                        currentPrisoners.CreatePrisonerButtons(moveablePrisoners);
                        
                        capacity.UpdatePrisonersHoused(currentPrisoners.GetPrisonersInPrison().Count);
                    }
                    
                    else
                    {
                        Debug.Log("Moveable prisoners array is null.");
                    }
                }
                else {
                    infoBox.UpdateInfoText("There's no more room in the prison! Upgrade the capacity of your prison.");
                }
        }
        }
    }

    // Move amount makes sure that user moves prisoners only once per day
    public void ResetMoveAmount() {
        moveAmount = 1;
    }
}

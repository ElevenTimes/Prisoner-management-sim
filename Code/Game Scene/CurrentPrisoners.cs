using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Linq;
public class CurrentPrisoners : MonoBehaviour
{   
    public Button buttonPrefab; // Reference to the button prefab
    public Transform buttonParent; // Parent transform for the buttons
    private List<Prisoner> prisonersInPrison = new List<Prisoner>();

    public InfoBox infoBox;

    public SecurityUpgrade securityUpgrade;

    void Start()
    {
        buttonPrefab.gameObject.SetActive(false);
    }

    
    public List<Prisoner> GetPrisonersInPrison()
    {   
    return prisonersInPrison;
    }
    
    public void PrintThis()
    {   
        foreach (Prisoner prisoner in prisonersInPrison) {
        }
    }
    public void AddLoadedPrisoners(Prisoner[] loadedPrisoners)
    {
        foreach (Prisoner prisoner in loadedPrisoners) 
        {
            if (!prisonersInPrison.Contains(prisoner))
            {
                prisonersInPrison.Add(prisoner);
            }
        }
        // Create buttons for the loaded prisoners
        CreatePrisonerButtons(prisonersInPrison.ToArray());
    }

    // When prisoners go from "intake" to "prison" they are actually destroyed in "intake" array and recreated in "prison" array
    public void CreatePrisonerButtons(Prisoner[] prisoners)
    {

        prisoners = prisoners.OrderBy(p => p.Sentence).ToArray();
        foreach (Prisoner prisoner in prisoners)
        {
            if (!prisonersInPrison.Contains(prisoner)) {
                prisonersInPrison.Add(prisoner);
            }
            Button newButton = Instantiate(buttonPrefab, buttonParent);

            // Get the TextMeshProUGUI components
            TextMeshProUGUI[] texts = newButton.GetComponentsInChildren<TextMeshProUGUI>();

            // Set the first TextMeshProUGUI component's text to the prisoner's name
            texts[0].text = prisoner.Name;

            // Set the second TextMeshProUGUI component's text to the prisoner's crime
            if (prisoner.Severity == Severity.Class_E) {
                texts[1].text = "Class E | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Class_D){
                texts[1].text = "Class D | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Class_C){
                texts[1].text = "Class C | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Class_B){
                texts[1].text = "Class B | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Class_A){
                texts[1].text = "Class A | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Financial_crime){
                texts[1].text = "Financial crime | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Cybercrime){
                texts[1].text = "Cybercrime | " + prisoner.Crime;
            }
            else if (prisoner.Severity == Severity.Capital_offense){
                texts[1].text = "Capital offense | " + prisoner.Crime;
            }

            texts[2].text = "Days left: " + prisoner.Sentence;

            newButton.onClick.AddListener(() => OnButtonClick(prisoner));

            // Make the instantiated button visible

            newButton.gameObject.SetActive(true);

            prisoner.Button = newButton;

        }
    }

    // Function for updating the prisoners sentence. Every time "Advance day" is presses the prisoners sentences need to be reduced by 1
    public void UpdatePrisonerButtons()
    {   
        prisonersInPrison = prisonersInPrison.OrderBy(prisoner => prisoner.Name).ToList();

        foreach (Prisoner prisoner in prisonersInPrison)
        {
            foreach (Transform buttonTransform in buttonParent)
            {
                Button button = buttonTransform.GetComponent<Button>();
                TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();

                if (texts[0].text == prisoner.Name)
                {
                    // Update the days left text
                    texts[2].text = "Days left: " + prisoner.Sentence;
                    break; // Break out of the inner loop once the button is found
                }
            }
        }
    }

    public void DestroyPrisonerButtons(Prisoner prisoner) {
        if (prisoner != null && prisoner.Button != null)
        {
            Destroy(prisoner.Button.gameObject);
            prisonersInPrison.Remove(prisoner);
        }
    }
    
    // Additional information about each prisoner
    void OnButtonClick(Prisoner prisoner)
    {
        if (securityUpgrade.GetSecurityLow() >= prisoner.EscapeOdds)
        {
            infoBox.UpdateInfoText("Name: " + prisoner.Name + ", Crime: " + prisoner.Crime + ", Sentence: " + prisoner.Sentence + ", Severity: " + (int)prisoner.Severity + ", Escape odds: 0%");
        }
        else 
        {
            infoBox.UpdateInfoText("Name: " + prisoner.Name + ", Crime: " + prisoner.Crime + ", Sentence: " + prisoner.Sentence + ", Severity: " + (int)prisoner.Severity + ", Escape odds: " + (prisoner.EscapeOdds / (securityUpgrade.GetSecurityHigh() / 100))  + "%");
        }
    }
}


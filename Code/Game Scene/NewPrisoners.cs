using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class NewPrisoners : MonoBehaviour
{   
    public Button buttonPrefab; // Reference to the button prefab
    public Transform buttonParent; // Parent transform for the buttons
    public ShowDays showdays;
    public SecurityUpgrade securityUpgrade;
    public InfoBox infoBox;

    public CurrentPrisoners currentPrisoners;

    private Prisoner[] randomPrisoners = new Prisoner[0];

    System.Random random = new System.Random();
    string prisonersFilePath = Path.Combine(Application.streamingAssetsPath, "prisoners.json");
    private bool sortBySeverity = true;
    void Start()
    {
        buttonPrefab.gameObject.SetActive(false);
    }

    // Main function responsible for receiving new prisoners
    public Prisoner[] AddNewPrisoners()
    {   
        int prisonerAmount = GetPrisonerAmount();
        
        // Initialize prisoners array with data
        Prisoner[] prisoners = InitializePrisoners();
        randomPrisoners = GenerateRandomPrisoners(prisoners, prisonerAmount);
        // Create buttons for each prisoner
        CreatePrisonerButtons(randomPrisoners);
        return randomPrisoners;
    }

    // Helper function
    public int GetPrisonerAmount() {

        int prisonerAmount = 0;
        if (showdays.GetDays() == 1) {
            prisonerAmount = 3;
        }
        else if (showdays.GetDays() >= 100){
            prisonerAmount = 10;
        }
        else if (showdays.GetDays() >= 50){
            prisonerAmount = random.Next(5, 9);
        }
        else if (showdays.GetDays() >= 20){
            prisonerAmount = random.Next(4, 8);
        }
        else {
            prisonerAmount = random.Next(2, 6);
        }

        return prisonerAmount;
    }

    // Creates random prisoners that follow the guidelines
    Prisoner[] GenerateRandomPrisoners(Prisoner[] prisoners, int amount)
    {
        // Filter prisoners based on Severity and whether they are already in prison
        Prisoner[] filteredPrisoners = prisoners.Where(p => ShouldIncludePrisoner(p) && !currentPrisoners.GetPrisonersInPrison().Any(prisoner => prisoner.Crime == p.Crime)).ToArray();

        // Ensure that the number of available prisoners is not less than the required amount
        int availablePrisonersCount = filteredPrisoners.Length;
        amount = Mathf.Min(amount, availablePrisonersCount);
        
        // Generate random prisoners
        Prisoner[] randomPrisoners = new Prisoner[amount];
        HashSet<int> chosenIndices = new HashSet<int>();

        for (int i = 0; i < amount; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = random.Next(0, availablePrisonersCount);
            } while (chosenIndices.Contains(randomIndex)); // Ensure unique random indices

            randomPrisoners[i] = filteredPrisoners[randomIndex];
            chosenIndices.Add(randomIndex);
        }

        return randomPrisoners;
    }

    // Method to determine if a prisoner should be included based on the current criteria
    bool ShouldIncludePrisoner(Prisoner prisoner)
    {   
        if (showdays.GetDays() == 1) 
        {
            return prisoner.Severity == Severity.Class_E;
        }
        else if (showdays.GetDays() <= 15)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D;
        }
        else if (showdays.GetDays() <= 30)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D || prisoner.Severity == Severity.Financial_crime;
        }
        else if (showdays.GetDays() <= 40)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D || prisoner.Severity == Severity.Class_C || prisoner.Severity == Severity.Financial_crime;
        }
        else if (showdays.GetDays() <= 50)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D || prisoner.Severity == Severity.Class_C || prisoner.Severity == Severity.Financial_crime || prisoner.Severity == Severity.Cybercrime;
        }
        else if (showdays.GetDays() <= 70)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D || prisoner.Severity == Severity.Class_C || prisoner.Severity == Severity.Class_B || prisoner.Severity == Severity.Financial_crime || prisoner.Severity == Severity.Cybercrime;
        }
        else if (showdays.GetDays() <= 100)
        {
            return prisoner.Severity == Severity.Class_E || prisoner.Severity == Severity.Class_D || prisoner.Severity == Severity.Class_C || prisoner.Severity == Severity.Class_B || prisoner.Severity == Severity.Class_A || prisoner.Severity == Severity.Financial_crime || prisoner.Severity == Severity.Cybercrime;
        }
        // Otherwise, include all prisoners
        return true;
    }

    // Initialize prisoner data
    Prisoner[] InitializePrisoners()
    {
        // Check if the JSON file exists
        if (File.Exists(prisonersFilePath))
        {   
            // Deserialize prisoners from JSON file
            string json = File.ReadAllText(prisonersFilePath);
            Prisoner[] prisoners = JsonConvert.DeserializeObject<Prisoner[]>(json);
            return prisoners; // Return the prisoners array
        }
        else
        {
            Debug.LogError("Prisoners JSON file not found!");
            return new Prisoner[0]; // Return an empty array if the file is not found
        }
    }

    // Create buttons for each prisoner. Buttons are the visual representation of prisoners and their properties in game
    void CreatePrisonerButtons(Prisoner[] prisoners)
    {   
        foreach (Prisoner prisoner in prisoners)
        {
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

            texts[2].text = "Sentence: " + prisoner.Sentence;

            newButton.onClick.AddListener(() => OnButtonClick(prisoner));

            // Add toggle component
            Toggle toggle = newButton.GetComponentInChildren<Toggle>();

            // Add listener to toggle
            toggle.onValueChanged.AddListener((value) => OnToggleValueChanged(value, prisoner));

            // Make the instantiated button visible
            newButton.gameObject.SetActive(true);
            prisoner.Button = newButton;
        }
    }

    public void DestroyPrisonerButtons(Prisoner[] prisoners) {
        foreach (Prisoner prisoner in prisoners)
        {
            if (prisoner.Button != null)
            {
                Destroy(prisoner.Button.gameObject);
                // Optionally, reset the button reference to null
                prisoner.Button = null;
            }
        }
    }

    // Function for sorting prisoners. Since there's no actual way to sort buttons in Unity, the function destroys them, sorts the prisoner array itself and then recreates them in the correct order
    public void SortPrisoners()
    {   
        Prisoner[] filteredPrisoners = randomPrisoners.Where(p => !currentPrisoners.GetPrisonersInPrison().Any(prisoner => prisoner.Crime == p.Crime)).ToArray();
        if (sortBySeverity) {
            filteredPrisoners = filteredPrisoners.OrderBy(p => (int)p.Severity).ToArray();
            sortBySeverity = false;
            infoBox.UpdateInfoText("Prisoners sorted by their Severity.");
        }
        else {
            filteredPrisoners = filteredPrisoners.OrderBy(p => p.Sentence).ToArray();
            sortBySeverity = true;
            infoBox.UpdateInfoText("Prisoners sorted by their Sentence.");
        }

        // Destroy existing buttons
        DestroyPrisonerButtons(filteredPrisoners);

        // Create buttons for sorted randomPrisoners
        CreatePrisonerButtons(filteredPrisoners);
    }


    // Method to handle toggle value change
    void OnToggleValueChanged(bool value, Prisoner prisoner)
    {
        // Update prisoner's property based on toggle value
        prisoner.IsSelected = value;
    }

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

// An enum method that handles prisoner severity. This way we can attach numbers to actual descriptive values
public enum Severity
{
    Class_E = 1,
    Class_D = 2,
    Class_C = 5,
    Class_B = 20,
    Class_A = 50,
    Financial_crime = 4,
    Cybercrime = 10,
    Capital_offense = 200,

}

// Prisoner class with properties
public class Prisoner
{
    public string Name { get; private set; }
    public string Crime { get; private set; }
    public int Sentence { get; private set; }
    public Severity Severity { get; private set; }
    public int EscapeOdds { get; private set; }
    public bool IsSelected { get; set; } // Property to track toggle state
    [JsonIgnore] // Json doesn't understand how to handle buttons, so we don't give them to it
    public Button Button { get; set; }

    // Constructor
    public Prisoner(string name, string crime, int sentence, Severity severity, int escapeOdds)
    {
        Name = name;
        Crime = crime;
        Sentence = sentence;
        Severity = severity;
        EscapeOdds = escapeOdds;
        IsSelected = false; 
        Button = null; 
    }

    public void ReduceSentence()
    {
        Sentence -= 1;
    }
}

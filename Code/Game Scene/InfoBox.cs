using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Used to display additional information and help user understand the game
public class InfoBox : MonoBehaviour
{
    public TMP_Text infoText;
    void Start()
    {
        infoText.text = "Hi, this is the info box. I'll help you figure stuff out. Click on me.";
    }

    public void UpdateInfoText(string newInfo) {

        Debug.Log(newInfo);
        if (infoText != null) {
            infoText.text = newInfo;
        }
        else {
            infoText.text = "Click on anything that you don't understand. I'll tell you what it is.";
        }
    }
}

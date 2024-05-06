using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPrisonTitleInfo : MonoBehaviour
{
    public InfoBox infoBox;
    public void OnMouseDown()
    {
        infoBox.UpdateInfoText("Each day, you'll receive money based on the severity of the crimes committed by the prisoners housed. Every prisoner housed has a daily chance to escape, so click on a prisoner for more information about them.");
    }
}

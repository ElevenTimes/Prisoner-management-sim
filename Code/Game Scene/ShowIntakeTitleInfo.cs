using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIntakeTitleInfo : MonoBehaviour
{   
    public InfoBox infoBox;
    public void OnMouseDown()
    {
        infoBox.UpdateInfoText("Each day, prisoners will be delivered as an intake to your prison. You can add these prisoners by selecting them and advancing to the next day. Each prisoner has different parameters, so click on them for more information."); 
    }
}

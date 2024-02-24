using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestinationTooltipTrigger : MonoBehaviour
{
    public void Show()
    {
        TooltipSystem.Show();
    }

    public void Hide()
    {
        TooltipSystem.Hide();
    }
}

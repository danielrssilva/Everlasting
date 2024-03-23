using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestinationTooltipTrigger : MonoBehaviour
{
    private Destination destination;

    public void SetDestination(Destination destination)
    {
        this.destination = destination;
    }

    public void Show()
    {
        TooltipSystem.Show(destination);
    }

    public void Hide()
    {
        TooltipSystem.Hide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public DestinationTooltip destinationTooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show()
    {
        current.destinationTooltip.SetData();
        current.destinationTooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.destinationTooltip.gameObject.SetActive(false);
    }
}

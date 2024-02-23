using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTooltipSystem : MonoBehaviour
{
    private static StopTooltipSystem current;

    public StopTooltip stopTooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(Stop stop)
    {
        current.stopTooltip.SetData(stop);
        current.stopTooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.stopTooltip.gameObject.SetActive(false);
    }
}

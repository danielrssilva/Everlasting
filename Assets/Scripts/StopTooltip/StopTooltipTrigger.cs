using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopTooltipTrigger : MonoBehaviour
{
    private Stop stop;

    public void SetStop(Stop stop)
    {
        this.stop = stop;
    }

    public void Show()
    {
        StopTooltipSystem.Show(stop);
    }

    public void Hide()
    {
        StopTooltipSystem.Hide();
    }
}

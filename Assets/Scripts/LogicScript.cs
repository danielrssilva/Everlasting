using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public Stop stop;

    public void ChangeNextStop(Stop _stop)
    {
        stop = _stop;
    }

    [ContextMenu("Decrease ETA")]
    public void UpdateETA()
    {
        stop.eta -= 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraReward : MonoBehaviour
{
    public TMP_Text label;

    public void SetData(string _label)
    {
        label.SetText(_label);
    }
}

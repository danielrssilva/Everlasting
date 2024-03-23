using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraDataContainer : MonoBehaviour
{
    public TMP_Text title;
    public GameObject rewardsContainer;

    public void SetData(string _title)
    {
        this.title.SetText(_title);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public string label;
    public string value;
    public RewardType type = RewardType.MAX_ENERGY;

    public void SetData(string _value, RewardType _type)
    {
        label = _type.ToString();
        value = _value;
        type = _type;
    }
}

public enum RewardType
{
    MAX_ENERGY,
    ENERGY,
    MAX_FOOD,
    FOOD,
    PASSENGER,
    CART,
    VISITED,
}
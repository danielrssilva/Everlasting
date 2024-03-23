using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DestinationWeatherData : ScriptableObject
{
    public float value;
    public DestinationWeatherType type;
    public string label;
}

public enum DestinationWeatherType
{
    FOOD,
    ENERGY
}
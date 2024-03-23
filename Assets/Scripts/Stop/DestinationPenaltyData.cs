using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DestinationPenaltyData : ScriptableObject
{
    public DestinationPenaltyType type;
    public string label;
}

public enum DestinationPenaltyType
{
    CART_DISABLER
}
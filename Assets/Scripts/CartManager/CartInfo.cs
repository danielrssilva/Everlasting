using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cart", menuName = "Cart")]
public class CartInfo : ScriptableObject
{
    public CartType type = CartType.EMPTY;

    [Header("Sprites")]
    public Sprite cartBackground;
    public Sprite constructingBackground;
    public Sprite disabledBackground;
    public Sprite cartTypeIcon;
    public Sprite productionOutputIcon;
    
    public int time;
}

public enum CartType
{
    EMPTY,
    FARM,
    HOUSING,
    REACTOR,
    BATTERY,
    EXTRA_ENGINE,
}
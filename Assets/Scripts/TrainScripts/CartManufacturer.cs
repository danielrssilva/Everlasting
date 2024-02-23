using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartManufacturer : MonoBehaviour
{
    public float foodCost;
    public float energyCost;
    public int time;

    public CartManufacturer()
    {
        foodCost = 0;
        energyCost = 0;
        time = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food setting")]
public class Energy : ScriptableObject
{
    public float output;
    public float current;
    public float storage;
}

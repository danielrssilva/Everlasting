using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stop : MonoBehaviour
{
    public float eta;
    public string placeName;
    public bool visited;
    public bool isDestination;
    public List<Reward> rewards;

    public void SetData(string placeName, List<Reward> rewards)
    {
        this.eta = 0;
        this.placeName = placeName;
        this.rewards = rewards;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

[RequireComponent(typeof(StopTooltipTrigger))]
[RequireComponent(typeof(DestinationTooltipTrigger))]
public class StopTile : MonoBehaviour
{
    public Vector3Int position;
    public Vector3 worldPosition;
    public bool isDestination = false;
    public Stop stop;
    public Destination destination;

    public void Init(Vector3Int position, Vector3 worldPosition, Stop stop)
    {
        this.position = position;
        this.worldPosition = worldPosition;
        this.stop = stop;
    }

    public void Init(Vector3Int position, Vector3 worldPosition, Destination destination)
    {
        this.position = position;
        this.worldPosition = worldPosition;
        this.destination = destination;
    }
}
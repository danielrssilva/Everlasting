using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Grid tileGrid;
    public GridInformation gridInformation;
    public Tilemap tilemap;

    public Tile passengerTile;
    public Tile visitedTile;
    public Tile cartTile;
    public Tile destinationTile;

    public StopTile CreateTile(Vector3Int position, Stop stop)
    {
        Tile tile = destinationTile;
        GameObject newTile = new GameObject();
        newTile.transform.SetParent(transform);
        newTile.AddComponent(typeof(StopTile));
        StopTile stopTile = newTile.GetComponent<StopTile>();
        stopTile.Init(position, tileGrid.CellToWorld(position), stop);
        gridInformation.SetPositionProperty(position, "StopTile", (UnityEngine.Object)newTile);
        
        switch (stop.rewards[0].type)
        {
            case RewardType.PASSENGER:
                tile = passengerTile;
                break;
            case RewardType.CART:
                tile = cartTile;
                break;
            default:
                break;
        }

        tilemap.SetTile(stopTile.position, tile);
        return stopTile;
    }

    public void CreateDestinationTile(Vector3Int position, Destination destination)
    {
        GameObject newTile = new GameObject();
        newTile.transform.SetParent(transform);
        newTile.AddComponent(typeof(StopTile));
        StopTile stopTile = newTile.GetComponent<StopTile>();
        stopTile.Init(position, tileGrid.CellToWorld(position), destination);
        gridInformation.SetPositionProperty(position, "DestinationTile", (UnityEngine.Object)newTile);
        tilemap.SetTile(stopTile.position, destinationTile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(StopTooltipTrigger))]
[RequireComponent(typeof(DestinationTooltipTrigger))]
public class TilemapController : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public GridInformation gridInformation;
    public Tile visitedTile;

    private StopTooltipTrigger stopTooltipTrigger;
    private DestinationTooltipTrigger destinationTooltipTrigger;
    private StopTile stopTile;
    private StopTile destinationTile;

    void Start()
    {
        grid = this.GetComponent<Grid>();
        stopTooltipTrigger = GetComponent<StopTooltipTrigger>();
        destinationTooltipTrigger = GetComponent<DestinationTooltipTrigger>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);

        if (tilemap.HasTile(gridPos))
        {
            GameObject o = new GameObject();
            stopTile = gridInformation.GetPositionProperty(gridPos, "StopTile", o).GetComponent<StopTile>();
            destinationTile = gridInformation.GetPositionProperty(gridPos, "DestinationTile", o).GetComponent<StopTile>();
            Destroy(o);
            if (stopTile != null)
            {
                stopTooltipTrigger.SetStop(stopTile.stop);
                stopTooltipTrigger.Show();
                if (Input.GetMouseButtonDown(0) && !stopTile.stop.visited)
                {
                    TrainManager.Instance.ClaimStopReward(stopTile.stop);
                    tilemap.SetTile(stopTile.position, visitedTile);
                }
            }
            if (destinationTile != null)
            {
                destinationTooltipTrigger.SetDestination(destinationTile.destination);
                destinationTooltipTrigger.Show();
                if (Input.GetMouseButtonDown(0))
                {
                    RoguelikeManager.Instance.ReachCurrentDestination();
                    tilemap.SetTile(destinationTile.position, visitedTile);
                }
            }
        }
        else
        {
            stopTooltipTrigger.Hide();
            destinationTooltipTrigger.Hide();
        }
    }
}
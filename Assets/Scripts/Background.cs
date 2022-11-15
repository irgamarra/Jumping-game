using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject mainCamera;
    //Background
    // 0 = "Next Tile"; 1 = "Tile"; 2 = "Previous Tile"
    public List<GameObject> backgroundTiles
        ;
    public float tilesLength = 12f;
    public float cameraOnTileRange = 3; // Range in which the camera will detect if it is on the next or previous tile
    void Start()
    {
        if (mainCamera == null)
        {
            throw new System.Exception("Main Camera not set.");
        }
        if (backgroundTiles == null || backgroundTiles.Count < 3)
        {
            throw new System.Exception("Background tiles not set or less than 3.");
        }
        else
        {
            AdjustTiles();
        }
    }

    private void Update()
    {
        //Main camera on "Next Tile"
        if (CameraOnTile(0))
        {
            ChangeTiles(1, 0, 2);
            AdjustTiles();
        }

        //Main camera on "Previous Tile"
        if (CameraOnTile(2))
        {
            ChangeTiles(1, 2, 0);
            AdjustTiles();
        }
    }

    private void AdjustTiles()
    {
        Vector3 indexTilePos = backgroundTiles[1].transform.position;
        backgroundTiles[0].transform.position = new Vector3(indexTilePos.x, indexTilePos.y + tilesLength, indexTilePos.z);
        backgroundTiles[2].transform.position = new Vector3(indexTilePos.x, indexTilePos.y - tilesLength, indexTilePos.z);
    }

    private bool CameraOnTile(int tileIndex)
    {
        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 tilePos = backgroundTiles[tileIndex].transform.position;

        if (cameraPos.y > tilePos.y - cameraOnTileRange && cameraPos.y < tilePos.y + cameraOnTileRange)
            return true;
        else
            return false;
    }

    private void ChangeTiles(int nextTileIndex, int tileIndex, int previousTileIndex)
    {
        GameObject nextTile = backgroundTiles[nextTileIndex].gameObject;
        nextTile.name = "Next Tile";
        GameObject tile = backgroundTiles[tileIndex].gameObject;
        tile.name = "Tile";
        GameObject previousTile = backgroundTiles[previousTileIndex].gameObject;
        previousTile.name = "Previous Tile";

        backgroundTiles[0] = nextTile;
        backgroundTiles[1] = tile;
        backgroundTiles[2] = previousTile;
    }



}

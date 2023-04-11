using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapSaveData
{
    public string mapName;
    public Grid.TileType[,] tileData;
    public int mapSizeX, mapSizeY;
    public MapSaveData(string _mapName, Grid.TileType[,] _saveTileData, Vector2Int _mapSize)
    {
        this.mapName = _mapName;
        this.tileData = _saveTileData;
        this.mapSizeX = _mapSize.x;
        this.mapSizeY = _mapSize.y;
    }

    public Vector2Int GetMapSize()
    {
        return new Vector2Int(mapSizeX,mapSizeY);
    }
}

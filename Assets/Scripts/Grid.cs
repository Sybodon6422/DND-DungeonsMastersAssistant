using System;
using UnityEngine;

public class Grid
{
    private TileData[,] position;

    public Grid(int xSize, int ySize, TileType[,] tileData)
    {
        position = new TileData[xSize,ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                position[x,y] = new TileData(tileData[x,y]);
            }
        }
    }

    public bool IsWalkable(int x, int y, Unit callingUnit)
    {
        return position[x,y].IsTileWalkable(callingUnit);
    }

    public bool GetIsOccupied(int x, int y)
    {
        bool occupied = (position[x,y].Occupier);
        return occupied;
    }
    public bool GetIsOccupied(Vector3Int pos)
    {
        bool occupied = (position[pos.x,pos.y].Occupier);
        return occupied;
    }

    public void SetIsOccupied(Unit occupier)
    {
        position[occupier.origin.x,occupier.origin.y].Occupier = occupier;
    }

    public void SetIsOccupied(int x, int y, Unit occupier)
    {
        position[x,y].Occupier = occupier;
    }
    public Unit GetOccupier(int x, int y){
        return position[x,y].Occupier;
    }

    public Unit GetOccupier(Vector3Int pos){
        return position[pos.x,pos.y].Occupier;
    }

    public struct TileData
    {
        public TileType tile;
        public Building building;
        public Unit Occupier;

        public bool IsTileWalkable(Unit unitCaller)
        {
            if(tile == TileType.grass || tile == TileType.forest || tile == TileType.sand || tile == TileType.road)
            {
                return true;
            }else{return false;}
        }

        public TileData(TileType typeOfTile)
        {
            tile = typeOfTile;
            building = null;
            Occupier = null;
        }
    }

    [Serializable]
    public enum TileType
    {
        grass,
        water,
        forest,
        sand,
        road,
        mountain
    }
}

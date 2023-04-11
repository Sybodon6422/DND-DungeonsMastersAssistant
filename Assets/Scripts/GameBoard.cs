using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    #region singleton

    private static GameBoard _instance;
    public static GameBoard Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public bool drawDebug = false;
    public void SetupGameBoard(Vector2Int newMapSize, Grid.TileType[,] savedTileGrid)
    {
        mapSize = newMapSize;
        grid = new Grid(mapSize.x,mapSize.y,savedTileGrid);

        validMovePositions = new List<Vector3Int>();
        validCombatPositions = new List<Vector3Int>();

        TurnManager.Instance.GameStart();

        if(drawDebug){DrawDebugCordnites();}
    }
    #endregion

    #region mapManagement 
    
    private Vector2Int mapSize;
    public Grid grid;
    public Tilemap worldTileMap, highlightTileMap;

    public Vector3 GetWorldPositionFromCell(Vector3Int cellPos)
    {
        return worldTileMap.CellToWorld(new Vector3Int(cellPos.x,cellPos.z,0));
    }
    public Vector3Int GetCellPositionFromWorld(Vector3 cellPos)
    {
        return worldTileMap.WorldToCell(cellPos);
    }

    public void ClearTileMap()
    {
        highlightTileMap.ClearAllTiles();
    }

    public bool IsWithinMapBounds(Vector3Int tilePos)
    {
    return tilePos.x >= 0 && tilePos.x < mapSize.x && tilePos.y >= 0 && tilePos.y < mapSize.y;
    }

    public Vector3Int GetRandomSpawnPoint(Unit starterUnit)
    {
        for (int i = 0; i < mapSize.x*mapSize.y; i++)
        {
            int randomXPos = Random.Range(1,mapSize.x);
            int randomYPos = Random.Range(1,mapSize.y);

            if(grid.IsWalkable(randomXPos,randomYPos, starterUnit))
            {
            return new Vector3Int(randomXPos,0,randomYPos);
            } 
        }
        Debug.Log("Could not find any valid spawn positions, this shouldnt be possible");
        return Vector3Int.zero;
    }

    public TileBase highlightTile, combatTile;

    #endregion

    #region Debugging
    public GameObject tileDebug;
    private Transform debugCornditesHolder;

    public void DrawDebugCordnites()
    {
        if(debugCornditesHolder)
        {
            for (int i = 0; i < debugCornditesHolder.childCount; i++)
            {
                Destroy(debugCornditesHolder.GetChild(i).gameObject);
            }
        }
        else
        {
            debugCornditesHolder = new GameObject().transform;
            debugCornditesHolder.name = "DebugCordnites";
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    var go = Instantiate(tileDebug, debugCornditesHolder);
                    go.transform.position = worldTileMap.CellToWorld(new Vector3Int(x,y,0));
                    WorldCordninates nites = go.GetComponent<WorldCordninates>();
                    nites.Setup(new Vector3Int(x,y,0));
                }
            }
        }
    }

    #endregion
   
    public void ShowMoveTiles(Unit callingUnit, List<Vector3Int> moves, List<Vector3Int> combatMoves)
    {
        UpdateValidMoveAndCombatPositions(callingUnit, moves, combatMoves, 1);

        if (validMovePositions.Count > 0 || validCombatPositions.Count > 0)
        {
        highlightTileMap.ClearAllTiles();
        SetHighlightTiles(highlightTile, validMovePositions);
        SetHighlightTiles(combatTile, validCombatPositions);
    }
    }

    private void SetHighlightTiles(TileBase tile, List<Vector3Int> positions)
    {
    foreach (var pos in positions)
    {
        highlightTileMap.SetTile(pos, tile);
    }
    }

    public void UpdateValidMoveAndCombatPositions(Unit callingUnit, List<Vector3Int> moves, List<Vector3Int> combatMoves, int faction)
    {
        validMovePositions.Clear();
        validCombatPositions.Clear();

        foreach (var move in moves)
        {
            if (IsWithinMapBounds(move) && !grid.GetIsOccupied(move))
            {   
                if(grid.IsWalkable(move.x,move.y,callingUnit)){
                validMovePositions.Add(move);
                }
            }
        }

        foreach (var combatMove in combatMoves)
        {
            if (IsWithinMapBounds(combatMove) && grid.GetIsOccupied(combatMove))
            {
                var occupier = grid.GetOccupier(combatMove.x, combatMove.y);
                if (occupier.faction != faction)
                {
                    validCombatPositions.Add(combatMove);
                }
            }
        }
    }

    public List<Vector3Int> GetPossibleCombatMoves(List<Vector3Int> allPossibleAttackPositions)
    {
        List<Vector3Int> possibleCombatMovesList = new List<Vector3Int>();
        foreach (var combatMove in allPossibleAttackPositions)
        {
            if (IsWithinMapBounds(combatMove) && grid.GetIsOccupied(combatMove))
            {
                var occupier = grid.GetOccupier(combatMove.x, combatMove.y);
                if (occupier.faction == 1)
                {
                    possibleCombatMovesList.Add(combatMove);
                }
            }
        }
        return possibleCombatMovesList;
    }

    private List<Vector3Int> validMovePositions;
    private List<Vector3Int> validCombatPositions;
    public bool ValidateMove(Vector3Int newPos){
        foreach (var potentialMove in validMovePositions)
        {
            if(newPos == potentialMove){return true;}

        }
        return false;
    }

    public bool ValidateCombatMove(Vector3Int combatPos){
        foreach (var potentialMove in validCombatPositions)
        {
            if(combatPos == potentialMove){return true;}

        }
        return false;
    }

    public void RoundPiecePosition(Piece piece){
        Vector3Int tilePos = worldTileMap.WorldToCell(piece.transform.position);
        piece.origin = tilePos;
        piece.transform.position = worldTileMap.CellToWorld(tilePos);
    }


    public bool AttemptMove(Unit pieceForMoveAttempt, Vector3Int targetTilePos)
    {
        if (IsWithinMapBounds(targetTilePos))
        {
            if (ValidateMove(targetTilePos))
            {
                // Clear the current grid position of the selected piece
                grid.SetIsOccupied(pieceForMoveAttempt.origin.x, pieceForMoveAttempt.origin.y, null);

                // Move the piece to the new tile position
                Vector3 fixedWorldPos = worldTileMap.CellToWorld(targetTilePos);
                pieceForMoveAttempt.Move(fixedWorldPos, targetTilePos);

                // Update the grid with the new position of the selected piece
                grid.SetIsOccupied(pieceForMoveAttempt);

                return true;
            }

            if (ValidateCombatMove(targetTilePos))
            {
                // Attack the occupier of the tile position if it exists
                var occupier = grid.GetOccupier(targetTilePos.x, targetTilePos.y);
                if (occupier != null)
                {
                    occupier.TakeDamage(pieceForMoveAttempt.Attack());
                    TurnManager.Instance.EndTurn();
                    return true;
                }
            }
        }
        
        return false;
    }
    
    public void SelectNewPiece(Unit newPiece)
    {
        if(newPiece.faction == 0)
        {
            ShowMoveTiles(newPiece, newPiece.ShowMoveTilesBySpeedRange(),newPiece.ShowMoveTilesByAttackRange());
        }
    }
}
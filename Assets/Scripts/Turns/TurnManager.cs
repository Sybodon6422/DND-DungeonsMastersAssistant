using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region singleton
    private static TurnManager _instance;
    public static TurnManager Instance { get { return _instance; } }
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
    #endregion

    public List<Turn> turnOrderList;
    private int currentTurn;
    void Start()
    {
        currentTurn = 0;
        allPieces = new List<Piece>();
    }


    public void GameStart()
    {
        foreach (var faction in turnOrderList)
        {
            CreateNewUnit(faction.unitGameObjects[0], faction.faction);
        }

        EndTurn();
    }

    public void NextTurn()
    {
        turnOrderList[0].MyTurn();
    }

    bool turnWaiting;
    private float currentWaitTime = 1;
    private void FixedUpdate()
    {
        if(turnWaiting){
            currentWaitTime -= Time.deltaTime*3;
            if(currentWaitTime <=0){
                turnWaiting = false;
                currentWaitTime = 1;
                NextTurn();
            }
        }
    }

    public void CreateNewUnit(GameObject unitFab, int faction)
    {
        var unitPiece = Instantiate(unitFab).GetComponent<Unit>();
        var tilePos = GameBoard.Instance.GetRandomSpawnPoint(unitPiece);
        unitPiece.ForceMove(GameBoard.Instance.GetWorldPositionFromCell(tilePos),tilePos);
        Debug.Log("TilePos: " + tilePos + " WorldPos: " + GameBoard.Instance.GetWorldPositionFromCell(tilePos));
        unitPiece.faction = faction;
    }

    public void UnitDied(Unit deadUnit)
    {
        turnOrderList[deadUnit.faction].UnitDied(deadUnit);
    }

    public void EndTurn()
    {
        GameBoard.Instance.ClearTileMap();
        turnWaiting = true;
        currentWaitTime = 1;
    }

    #region Piece management

    private List<Piece> allPieces;

    public void AddPiece(Unit newPiece)
    {
        if(allPieces.Contains(newPiece)){return;}
        allPieces.Add(newPiece);
        turnOrderList[newPiece.faction].units.Add(newPiece);
    }

    public List<Piece> EnemyPieces()
    {
        List<Piece> foundEnemyPieces = new List<Piece>();
        foreach (var _Piece in allPieces)
        {
            if(_Piece.faction > 1){
                foundEnemyPieces.Add(_Piece);
            }
        }

        return foundEnemyPieces;
    }
    public List<Piece> FriendlyPieces()
    {
        List<Piece> foundEnemyPieces = new List<Piece>();
        foreach (var _Piece in allPieces)
        {
            if(_Piece.faction == 1){
                foundEnemyPieces.Add(_Piece);
            }
        }

        return foundEnemyPieces;
    }

    #endregion
}

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

        playerManager = PlayerManager.Instance;
        enemyManager = GetComponent<EnemyManager>();
    }
    #endregion

    private int currentTurn;

    private PlayerManager playerManager;
    private EnemyManager enemyManager;

    void Start()
    {
        currentTurn = 0;
        allPieces = new List<Piece>();
    }


    public void GameStart()
    {

    }

    public void NextTurn()
    {
    }
    bool enemyTurn;
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

    public void CreateNewUnit(GameObject unitFab)
    {
        var unitPiece = Instantiate(unitFab).GetComponent<Unit>();
        var tilePos = GameBoard.Instance.GetRandomSpawnPoint(unitPiece);
        unitPiece.ForceMove(GameBoard.Instance.GetWorldPositionFromCell(tilePos),tilePos);
        Debug.Log("TilePos: " + tilePos + " WorldPos: " + GameBoard.Instance.GetWorldPositionFromCell(tilePos));
        //if we end up using this function we'll need to set the faction
    }

    public void UnitDied(Unit deadUnit)
    {

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
    }

    public List<Piece> EnemyPieces()
    {
        List<Piece> foundEnemyPieces = new List<Piece>();
        foreach (var _Piece in allPieces)
        {
            if(!_Piece.playerFaction){
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
            if(_Piece.playerFaction){
                foundEnemyPieces.Add(_Piece);
            }
        }

        return foundEnemyPieces;
    }

    #endregion
}

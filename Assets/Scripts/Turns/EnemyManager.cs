using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Turn
{
    private Unit currentTurnPiece;
    private List<Vector3Int> possibleMoves;
    private List<Vector3Int> possibleCombatMoves;
    public void MyTurn(Unit pieceForTurn)
    {
        currentTurnPiece = pieceForTurn;
        possibleCombatMoves = new List<Vector3Int>();
        possibleMoves = new List<Vector3Int>();
        MakeClosestToEnemyMove();
    }

    private void GetAllPossibleMoves()
    {
        possibleMoves = currentTurnPiece.ShowMoveTilesBySpeedRange();
        possibleCombatMoves = GameBoard.Instance.GetPossibleCombatMoves(currentTurnPiece.ShowMoveTilesByAttackRange());
    }

    private void MakeClosestToEnemyMove()
    {
        if (currentTurnPiece == null)
        {
            return;
        }

        // Get all possible moves for the current piece
        GetAllPossibleMoves();
        GameBoard.Instance.UpdateValidMoveAndCombatPositions(currentTurnPiece, possibleMoves,possibleCombatMoves, 2);
        // Find the closest enemy to the current piece
        Vector3Int closestEnemyPosition = FindClosestEnemyPosition(currentTurnPiece);

        // Find the move that gets the piece closest to the closest enemy
        Vector3Int closestMove = Vector3Int.zero;
        float closestDistance = float.MaxValue;
        foreach (Vector3Int move in possibleMoves)
        {
            float distanceToEnemy = Vector3Int.Distance(move, closestEnemyPosition);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestMove = move;
            }
        }

        if(possibleCombatMoves.Count >= 1)
        {
            if(GameBoard.Instance.AttemptMove(currentTurnPiece, possibleCombatMoves[0]))
            {
                return;
            }
        }

        // Make the closest move
        if (closestMove != Vector3Int.zero)
        {
            if(GameBoard.Instance.AttemptMove(currentTurnPiece, closestMove))
            {
                return;
            }
        }

        //if we cant make the closest move or attack we move at random;
        foreach (var item in possibleMoves)
        {
            if(GameBoard.Instance.AttemptMove(currentTurnPiece, item)){
                return;
            }
        }
        GameBoard.Instance.AttemptMove(currentTurnPiece, possibleMoves[Random.Range(0,possibleCombatMoves.Count)]);
        Debug.Log("Something fucked up");
    }

    private Vector3Int FindClosestEnemyPosition(Piece callingUnit)
    {
        List<Piece> enemyPieces;

        if(callingUnit.faction == 1)
        {
            enemyPieces = TurnManager.Instance.EnemyPieces();
        }
        else
        {
            enemyPieces = TurnManager.Instance.FriendlyPieces();
        }

        if (enemyPieces.Count == 0) return Vector3Int.zero; // Return zero vector if no enemy pieces found

        Piece closestEnemy = enemyPieces[0];
        Vector3Int closestEnemyPosition = closestEnemy.origin;
        float closestEnemyDistance = Vector3Int.Distance(callingUnit.origin, closestEnemyPosition);

    for (int i = 1; i < enemyPieces.Count; i++)
    {
        float enemyDistance = Vector3Int.Distance(callingUnit.origin, enemyPieces[i].origin);

        if (enemyDistance < closestEnemyDistance)
        {
            closestEnemy = enemyPieces[i];
            closestEnemyPosition = closestEnemy.origin;
            closestEnemyDistance = enemyDistance;
        }
    }

    return closestEnemyPosition;
    }

    public void EndTurn()
    {
        possibleCombatMoves = new List<Vector3Int>();
        possibleMoves = new List<Vector3Int>();

        if(currentTurnPiece)
        {
        currentTurnPiece = null;
        }
    }

}

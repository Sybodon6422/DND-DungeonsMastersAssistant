using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
     #region singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
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

    private Unit currentTurnPiece;

    public void EndTurn()
    {
        currentTurnPiece = null;
    }
    
    public void MyTurn(Unit pieceForTurn)
    {
        currentTurnPiece = pieceForTurn;
        GameBoard.Instance.SelectNewPiece(currentTurnPiece);
    }
    public void LeftClick(Vector3Int tilePos)
    {
        if (currentTurnPiece == null) {return;}
        GameBoard.Instance.AttemptMove(currentTurnPiece, tilePos);

    }
    public void RightClick(Vector2 mousePos){}
}

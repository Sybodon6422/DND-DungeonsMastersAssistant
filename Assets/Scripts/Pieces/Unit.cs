using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Piece
{
    [SerializeField] StatBlock stats;
    public StatBlock GetStats(){return stats;}
    public int weaponBonus = 0;
    public Weapon weapon;
    public int health;
    public int speed;
    public int attackRange;
    [SerializeField] private SpriteRenderer body;

    void Start()
    {
        TurnManager.Instance.AddPiece(this);
        speed = stats.speed/5;
        health = stats.maxHealth;
    }

    public void GameStart()
    {
        if(playerFaction){body.color = Color.blue;}else{body.color = Color.red;}
        GameBoard.Instance.RoundPiecePosition(this);
        GameBoard.Instance.grid.SetIsOccupied(origin.x,origin.y,this);
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        if(hasTarget)
        {
            transform.position = transform.position + (targetPosition - transform.position).normalized *Time.deltaTime*3;
            if(Vector3.Distance(transform.position, targetPosition) <= .1f)
            {
                transform.position = targetPosition;
                hasTarget = false;
                TurnManager.Instance.EndTurn();
            }
        }
    }

    public void SkipTurn(){TurnManager.Instance.EndTurn();}

    bool hasTarget = false;
    private Vector3 targetPosition;
    public void Move(Vector3 newPos , Vector3Int cellPos)
    {
        targetPosition = newPos;
        hasTarget = true;
        origin = cellPos;
        //GameBoard.Instance.ShowMoveTiles(GetMoves(speed), ShowMoveTilesByAttackRange());
    }

    public void ForceMove(Vector3 newPos , Vector3Int cellPos)
    {
        transform.position = newPos;
        origin = cellPos;
    }
    
    public List<Vector3Int> ShowMoveTilesBySpeedRange()
    {
        return GetMoves(speed);
    }
    public List<Vector3Int> ShowMoveTilesByAttackRange()
    {
        return GetMoves(attackRange);
    }

    public List<Vector3Int> GetMoves(int range)
    {
        List<Vector3Int> movePositions = GetPositionsList(origin);

        if(range > 1)
            {
            for(int i = 1; i < range; i++)
            {
                List<Vector3Int> nextPositions = new List<Vector3Int>();
                foreach(Vector3Int position in movePositions)
                    {
                    List<Vector3Int> adjPositions = GetPositionsList(position);
                    foreach(Vector3Int adjPosition in adjPositions)
                        {
                        if(!movePositions.Contains(adjPosition) && !nextPositions.Contains(adjPosition))
                            {
                            nextPositions.Add(adjPosition);
                            }
                        }
                    }
            movePositions.AddRange(nextPositions);
            }
        }
        return movePositions;
    }

    private List<Vector3Int> GetPositionsList(Vector3Int intialPos)
    {
        List<Vector3Int> movePositions = new List<Vector3Int>();
        if(intialPos.y % 2 == 0)
        {
            movePositions.Add(new Vector3Int(intialPos.x + 1,  intialPos.y,       0)); // move up
            movePositions.Add(new Vector3Int(intialPos.x,      intialPos.y + 1,   0)); // move right
            movePositions.Add(new Vector3Int(intialPos.x - 1,  intialPos.y,       0)); // move down

            movePositions.Add(new Vector3Int(intialPos.x - 1,  intialPos.y - 1,   0)); //bottm right
            movePositions.Add(new Vector3Int(intialPos.x,      intialPos.y - 1,   0)); // move left
            movePositions.Add(new Vector3Int(intialPos.x - 1,  intialPos.y + 1,   0)); // top left
        }
        else
        {
            movePositions.Add(new Vector3Int(intialPos.x + 1,  intialPos.y,       0)); // move up
            movePositions.Add(new Vector3Int(intialPos.x,      intialPos.y + 1,   0)); // move right
            movePositions.Add(new Vector3Int(intialPos.x - 1,  intialPos.y,       0)); // move down

            movePositions.Add(new Vector3Int(intialPos.x + 1,  intialPos.y + 1,   0)); //bottm right
            movePositions.Add(new Vector3Int(intialPos.x,      intialPos.y - 1,   0)); // move left
            movePositions.Add(new Vector3Int(intialPos.x + 1,  intialPos.y - 1,   0)); // top left
        }

        return movePositions;
    }

    public virtual List<Vector3Int> GetCombatMoves()
    {

        return null;
    }

    public virtual int Attack()
    {
        return 0;
    }

    public void TakeDamage(int damage){
        health -= damage;
        GetComponent<PieceHudController>().UpdateHealthBar(health,stats.maxHealth);
        if(health <= 0 )
        {
            GameBoard.Instance.grid.SetIsOccupied(origin.x,origin.y,null);
            TurnManager.Instance.UnitDied(this);
            Destroy(gameObject);
        }else{
            TurnManager.Instance.EndTurn();
        }
    }
}

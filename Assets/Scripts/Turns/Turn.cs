using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turn
{
    public int faction;
    public List<Unit> units;

    public List<GameObject> unitGameObjects;

    public void RequestUnitSpawn()
    {
    }

    public void SetupFaction()
    {

    }

    public virtual void MyTurn()
    {
        if(faction == 0)
        {
            PlayerManager.Instance.MyTurn(units[0]);
        }
    }

    public void UnitDied(Unit deadUnit)
    {
        units.Remove(deadUnit);
    }
}

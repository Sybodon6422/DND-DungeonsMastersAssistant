using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{
    public int damageDealt;

    public override int Attack()
    {
        return damageDealt;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCalculator : MonoBehaviour
{
    private bool weaponProfBonus = true;
    private int weaponBonus = 4;
    public void RollD10()
    {
        var hitRoll = Dice.RollD20();
        int profBonus = 0;
        if(weaponProfBonus){profBonus = 2;}
        int total = hitRoll + profBonus + weaponBonus;
        if(total > 15)
        {
            GameConsole.Instance.PrintToConsole("Hit! Unit rolled " + hitRoll + " + " + (profBonus+weaponBonus) +" ("+total+")" );
                        
            var damageRoll = Dice.RollD10();
            GameConsole.Instance.PrintToConsole("Unit Dealt " + damageRoll + " + " + (weaponBonus) +" ("+(damageRoll+weaponBonus)+")" );
        }else
        {
            GameConsole.Instance.PrintToConsole("Miss! Unit rolled " + hitRoll + " + " + (profBonus+weaponBonus) +" ("+total+")" );
        }
    }

    public void DoesHit(StatBlock attacker,StatBlock deffender, int attackerWeaponDieRoll, int attackerWeaponBonus)
    {
        
    }
}

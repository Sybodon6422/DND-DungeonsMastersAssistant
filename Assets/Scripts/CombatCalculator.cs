using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatCalculator
{
    public static bool DoesHit(Advantage advantage, StatBlock attacker,StatBlock deffender, int attackerWeaponBonus)
    {
        int hitRoll = RollForHit(advantage);

        int profBonus = 0;
        if(attacker.simpleWeaponsProficiency){profBonus = attacker.proficiencyBonus;}
        int total = hitRoll + profBonus + attackerWeaponBonus;
        if(total > deffender.armorClass)
        {
            GameConsole.Instance.PrintToConsole("Hit! Unit rolled " + hitRoll + " + " + (profBonus+attackerWeaponBonus) +" ("+total+")" );
            return true;
        }
        else
        {
            GameConsole.Instance.PrintToConsole("Miss! Unit rolled " + hitRoll + " + " + (profBonus+attackerWeaponBonus) +" ("+total+")" );
            return false;
        }
    }

    public static int DamageDealtMelee(StatBlock attacker, Weapon weapon, bool usingVersatile, int bonus)
    {
        var damageRoll = 0;
        if(!usingVersatile)
        {
            for (int i = 0; i < weapon.diceAmmount; i++)
            {
                damageRoll += Dice.Roll(weapon.dice);
            }
        }
        else
        {
            for (int i = 0; i < weapon.versatileWeaponDiceAmmount; i++)
            {
                damageRoll += Dice.Roll(weapon.versatileWeaponDice);
            }
        }

        GameConsole.Instance.PrintToConsole("Unit dealt " + (damageRoll + bonus) + "damage!");
        return (damageRoll + bonus);
    }
    
    private static int DiceRefByInt(int refNumber)
    {
        int diceRollInt = 0;
        switch (refNumber)
        {
            case 100:
                diceRollInt = Dice.RollD100();
                break;
            case 20:
                diceRollInt = Dice.RollD20();
                break;
            case 12:
                diceRollInt = Dice.RollD12();
                break;
            case 10:
                diceRollInt = Dice.RollD10();
                break;
            case 8:
                diceRollInt = Dice.RollD8();
                break;
            case 6:
                diceRollInt = Dice.RollD6();
                break;
            case 4:
                diceRollInt = Dice.RollD4();
                break;
        }
        return diceRollInt;
    }

    private static int RollForHit(Advantage advantage)
    {
        switch (advantage)
        {
            case Advantage.advantage:
            {
                var hitCheck1 = Dice.RollD20();
                var hitCheck2 = Dice.RollD20();

                if(hitCheck2 > hitCheck1)
                {
                    return hitCheck2;
                }
                else
                {
                    return hitCheck1;
                }
            }
            case Advantage.disadvantage:
            {
                var hitCheck1 = Dice.RollD20();
                var hitCheck2 = Dice.RollD20();

                if(hitCheck2 > hitCheck1)
                {
                    return hitCheck1;
                }
                else
                {
                    return hitCheck2;
                }
            }
            case Advantage.noadvantage:
            {
                return Dice.RollD20();
            }
        }

        return Dice.RollD20();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatCalculator
{
    public static HitType DoesHit(Advantage advantage, StatBlock attacker, StatBlock deffender, int attackerWeaponBonus, bool hasProficiency, int abilityMod)
    {
        int hitRoll = RollForHit(advantage);
        int profBonus = 0;
        if (hasProficiency) { profBonus = attacker.proficiencyBonus; }
        int total = hitRoll + abilityMod + profBonus + attackerWeaponBonus;
        if (hitRoll == 20)
        {
            GameConsole.Instance.PrintToConsole("Critical Hit!" + attacker.name + hitRoll + " + " + (profBonus + attackerWeaponBonus + abilityMod) + " (" + total + ")");
            return HitType.CriticalHit;
        }
        if (total > deffender.GetArmorClass())
        {
            GameConsole.Instance.PrintToConsole("Hit!" + attacker.name + hitRoll + " + " + (profBonus + attackerWeaponBonus + abilityMod) + " (" + total + ")" + "DF AC: " + deffender.GetArmorClass());
            return HitType.Hit;
        }
        else
        {
            GameConsole.Instance.PrintToConsole("Miss!" + attacker.name + hitRoll + " + " + (profBonus + attackerWeaponBonus + abilityMod) + " (" + total + ")");
            return HitType.Miss;
        }
    }

    public static int DamageDealtMelee(StatBlock attacker, Weapon weapon, bool usingVersatile, int bonus, bool criticalHit, int abilityMod)
    {
        var damageRoll = 0;
        int diceAmmount;
        if (criticalHit)
        {
            diceAmmount = weapon.diceAmmount*2;
        }
        else
        {
            diceAmmount = weapon.diceAmmount;
        }
        if (!usingVersatile)
        {
            for (int i = 0; i < diceAmmount; i++)
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

        GameConsole.Instance.PrintToConsole("Unit dealt " + (damageRoll + bonus + abilityMod) + "damage!");
        return (damageRoll + bonus + abilityMod);
    }

    public static int DamageDealtMelee(StatBlock attacker, CROSSEnemyDataCreature.CreatureAttack creatureAttack, int bonus, bool criticalHit, int abilityMod)
    {
        var damageRoll = 0;
        int diceAmmount;
        if (criticalHit)
        {
            diceAmmount = creatureAttack.damageDiceAmmount * 2;
        }
        else
        {
            diceAmmount = creatureAttack.damageDiceAmmount;
        }
        for (int i = 0; i < diceAmmount; i++)
        {
            damageRoll += Dice.Roll(creatureAttack.damageDice);
        }

        GameConsole.Instance.PrintToConsole("Creature dealt " + (damageRoll + bonus + abilityMod) + "damage!", Color.red);
        return (damageRoll + bonus + abilityMod);
    }

    private static int RollForHit(Advantage advantage)
    {
        switch (advantage)
        {
            case Advantage.advantage:
                {
                    var hitCheck1 = Dice.RollD20();
                    var hitCheck2 = Dice.RollD20();

                    if (hitCheck2 > hitCheck1)
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

                    if (hitCheck2 > hitCheck1)
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

    public enum HitType
    {
        Hit,
        Miss,
        CriticalHit
    }
}

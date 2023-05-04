using System;

[Serializable]
public class StatBlock
{
    public int maxHealth;
    public int armorClass;
    public int speed;

    public int strength = 8, dexterity = 8,constitution = 8,intelligence = 8,wisdom = 8,charisma = 8;

    public int proficiencyBonus = 2;

    public bool simpleWeaponsProficiency, martialWeaponsProficiency;
}

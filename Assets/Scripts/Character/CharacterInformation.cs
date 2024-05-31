using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterInformation;

public static class CharacterInformation
{
    public enum Ability
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public enum CharacterRace
    {
        Dragonborn,
        Dwarf,
        Elf,
        Gnome,
        HalfElf,
        Halfing,
        HalfOrc,
        Human,
        Tiefling,
        Aarakocra,
        Aasimar,
    }
    public enum CharacterClass
    {
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Warlock,
        Wizard
    }

    public static int GetBaseHealthByClass(CharacterClass classToCheck)
    {
        switch (classToCheck)
        {
            case CharacterClass.Barbarian:
                return 12;
            case CharacterClass.Bard:
                return 8;
            case CharacterClass.Cleric:
                return 8;
            case CharacterClass.Druid:
                return 8;
            case CharacterClass.Fighter:
                return 10;
            case CharacterClass.Monk:
                return 8;
            case CharacterClass.Paladin:
                return 10;
            case CharacterClass.Ranger:
                return 10;
            case CharacterClass.Rogue:
                return 8;
            case CharacterClass.Sorcerer:
                return 6;
            case CharacterClass.Warlock:
                return 8;
            case CharacterClass.Wizard:
                return 6;
        }
        return 0;
    }
}

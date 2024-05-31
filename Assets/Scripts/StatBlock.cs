using System;

[Serializable]
public class StatBlock
{
    public int maxHealth;
    public int armorClass;
    public int speed;

    public int strength = 8, dexterity = 8, constitution = 8, intelligence = 8, wisdom = 8, charisma = 8;

    public int proficiencyBonus = 2;

    public bool simpleWeaponsProficiency, martialWeaponsProficiency;

    public void StatBlockFromClass(CharacterInformation.CharacterRace race, int[] defaultStatBlock)
    {
        switch (race)
        {
            case CharacterInformation.CharacterRace.Dragonborn:
                strength = defaultStatBlock[0] + 2; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5] + 1;
                break;
            case CharacterInformation.CharacterRace.Dwarf:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2] + 2; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.Elf:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1] + 2; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.Gnome:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3] + 2; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.HalfElf:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5] + 2;
                break;
            case CharacterInformation.CharacterRace.Halfing:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1] + 2; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.HalfOrc:
                strength = defaultStatBlock[0] + 2; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2] + 1; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.Human:
                strength = defaultStatBlock[0] + 1; dexterity = defaultStatBlock[1] + 1; constitution = defaultStatBlock[2] + 1; intelligence = defaultStatBlock[3] + 1; wisdom = defaultStatBlock[4] + 1; charisma = defaultStatBlock[5] + 1;
                break;
            case CharacterInformation.CharacterRace.Tiefling:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3] + 1; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5] + 2;
                break;
            case CharacterInformation.CharacterRace.Aarakocra:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1] + 2; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4] + 1; charisma = defaultStatBlock[5];
                break;
            case CharacterInformation.CharacterRace.Aasimar:
                strength = defaultStatBlock[0]; dexterity = defaultStatBlock[1]; constitution = defaultStatBlock[2]; intelligence = defaultStatBlock[3]; wisdom = defaultStatBlock[4]; charisma = defaultStatBlock[5];
                break;
            default:
                break;

        }

    }
}

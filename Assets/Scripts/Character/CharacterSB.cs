using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSB
{
    public string characterName;
    public StatBlock characterStatBlock;

    public string raceName;
    public string className;

    public int level;

    public CharacterSB(string _charName, StatBlock _charStatBlock, string _race, string _class)
    {
        this.characterName = _charName;
        this.characterStatBlock = _charStatBlock;
        this.raceName = _race;
        this.className = _class;

        level = 1;
    }
}

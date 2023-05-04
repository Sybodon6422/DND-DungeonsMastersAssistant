using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "CharacterStuff/Race", order = 1)]
public class CharacterRace : ScriptableObject
{
    public int strBonus,dexBonus,conBonus,intBonus,wisBonus,chrBonus;
}
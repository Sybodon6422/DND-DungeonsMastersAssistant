using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Items/Armour", order = 1)]
public class ArmourItem : Item
{
    public int armourClassBonus = 0;
    public bool stealthAdvantage;
    public int strengthRequirement = 0;
    public int GetArmourBonus(int dexBonus)
    {
        int armorBonusActual = 0;
        switch (armourType)
        {
            case ArmorType.light:
                armorBonusActual = armourClassBonus + dexBonus;
                break;
                case ArmorType.medium:
                armorBonusActual = armourClassBonus + Mathf.Clamp(dexBonus,-10,2);
                break;
                case ArmorType.heavy:
                armorBonusActual = armourClassBonus;
                break;
        }
        return armorBonusActual;
    }

    public ArmorType armourType;
    public enum ArmorType
    {
        light,
        medium,
        heavy
    }
}

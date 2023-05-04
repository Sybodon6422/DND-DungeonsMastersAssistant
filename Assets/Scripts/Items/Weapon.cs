using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons", order = 1)]
public class Weapon : Item
{
    public int diceAmmount = 1;
    public DiceRoll dice;
    public DamageType damageType;
    public WeaponType weaponType;


    public bool finesse,heavy,light,loading,reach,special,twoHanded;
    [HideInInspector] public bool versatile, ammunition,thrown;
    [HideInInspector]public Vector2Int rangedWeaponRange;
    [HideInInspector]public Ammunition requiredAmmoType;
    [HideInInspector]public int versatileWeaponDiceAmmount =1;
    [HideInInspector]public DiceRoll versatileWeaponDice;

    public enum DamageType
    {
        bludgeoning,
        piercing,
        slashing
    }

    public enum WeaponType
    {
        simpleWeapon,
        martialWeapon,
    }
}

[CustomEditor(typeof(Weapon))]
public class WeaponCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var weapon = target as Weapon;

        weapon.versatile = GUILayout.Toggle(weapon.versatile, "Versatile");
        if(weapon.versatile)
        {
            weapon.versatileWeaponDiceAmmount = EditorGUILayout.IntField("Hit Die Ammount",1);
            weapon.versatileWeaponDice = (DiceRoll)EditorGUILayout.EnumPopup("Dice",weapon.versatileWeaponDice);
        }

        weapon.ammunition = GUILayout.Toggle(weapon.ammunition, "Ammunition");
        weapon.thrown = GUILayout.Toggle(weapon.thrown, "Thrown");
        if(weapon.ammunition || weapon.thrown)
        {
            weapon.rangedWeaponRange = EditorGUILayout.Vector2IntField("Min/Max Range", weapon.rangedWeaponRange);
        }
        if(weapon.ammunition)
        {
            weapon.requiredAmmoType = (Ammunition)EditorGUILayout.ObjectField("Ammo Type", weapon.requiredAmmoType, typeof(Ammunition),true);
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(weapon);
        }
    }
}

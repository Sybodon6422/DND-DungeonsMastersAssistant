using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ArmourItem equipArmor;
    public Weapon equipWeapon;
    public bool shield = false;
    public List<Item> inventoryItems;


    
    public bool InventoryContainsItem(Item checkItem)
    {
        if(inventoryItems.Contains(checkItem))
        {
            return true;
        }else
        {
            return false;
        }
    }
}

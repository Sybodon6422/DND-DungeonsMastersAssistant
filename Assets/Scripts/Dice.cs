using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice
{
    public static bool FlipCoin()
    {
        if(Random.Range(0,100)>50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int RollD4()
    {
        return Random.Range(0,4)+1;
    }

    public static int RollD4(int ammount)
    {
        return GetRandomNumber(4,ammount);
    }

    public static int RollD6()
    {
        return Random.Range(0,6)+1;
    }

    public static int RollD6(int ammount)
    {
        return GetRandomNumber(6,ammount);
    }

    public static int RollD8()
    {
        return Random.Range(0,8)+1;
    }

    public static int RollD8(int ammount)
    {
        return GetRandomNumber(8,ammount);
    }

    public static int RollD10()
    {
        return Random.Range(0,10)+1;
    }

    public static int RollD10(int ammount)
    {
        return GetRandomNumber(10,ammount);
    }

    public static int RollD12()
    {
        return Random.Range(0,12)+1;
    }

    public static int RollD12(int ammount)
    {
        return GetRandomNumber(12,ammount);
    }

    public static int RollD20()
    {
        return Random.Range(0,20)+1;
    }

    public static int RollD20(int ammount)
    {
        return GetRandomNumber(20,ammount);
    }

    public static int RollD100()
    {
        return Random.Range(0,100)+1;
    }

    private static int GetRandomNumber(int max, int diceAmmount)
    {
        int roll = 0;
        for (int i = 0; i < diceAmmount; i++)
        {
            roll += Random.Range(0,max)+1;
        }
        return roll;
    }
}

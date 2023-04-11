using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public string saveName;

    public GameSaveData(String _saveName)
    {
        this.saveName = _saveName;
    }
}

[Serializable]
public class ColourSerializable
{
    public float red, green, blue;

    public Color ConvertToColor()
    {
        Color color = new Color(red, green, blue);
        return color;
    }

    public ColourSerializable(Color color)
    {
        this.red = color.r;
        this.green = color.g;
        this.blue = color.b;
    }
}

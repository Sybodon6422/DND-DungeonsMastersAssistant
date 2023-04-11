using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MapSaveHandler
{
    string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public void SaveGame(MapSaveData saveData)
    {
        if (!File.Exists(fileLocation + "/NewLands/"))
        {
            Directory.CreateDirectory(fileLocation + "/NewLands/");

            if (!File.Exists(fileLocation + "/NewLands/Maps/"))
            {
                Directory.CreateDirectory(fileLocation + "/NewLands/Maps/");
            }
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileLocation + "/NewLands/Maps/" + saveData.mapName + ".dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Map Saved");
    }

    public MapSaveData LoadGame(String _saveName)
    {
        if (File.Exists(fileLocation + "/NewLands/Maps/" + _saveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(fileLocation + "/NewLands/Maps/" + _saveName + ".dat", FileMode.Open);
            MapSaveData data = (MapSaveData)bf.Deserialize(file);
            file.Close();

            return data;
        }
            
        Debug.LogError("There is no save data!");
        return null;
    }
    public MapSaveData LoadFirstMap()
    {
        var saveFiles = Directory.GetFiles(fileLocation + "/NewLands/Maps/");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   File.Open(saveFiles[0], FileMode.Open);
        MapSaveData data = (MapSaveData)bf.Deserialize(file);
        file.Close();
        return data;
    }
}
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

public class GameDataHandler
{
    string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public void SaveGame(String _saveName)
    {
        if (!File.Exists(fileLocation + "/UnityProjectSaves/"))
        {
            Directory.CreateDirectory(fileLocation + "/UnityProjectSaves/");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileLocation + "/UnityProjectSaves/" + _saveName + ".dat");
        GameSaveData data = new GameSaveData(_saveName);

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public void SaveGame(GameSaveData saveData)
    {
        if (!File.Exists(fileLocation + "/UnityProjectSaves/"))
        {
            Directory.CreateDirectory(fileLocation + "/UnityProjectSaves/");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileLocation + "/UnityProjectSaves/" + saveData.saveName + ".dat");

        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public GameSaveData LoadGame(String _saveName)
    {
        if (File.Exists(fileLocation + "/UnityProjectSaves/" + _saveName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(fileLocation + "/UnityProjectSaves/" + _saveName + ".dat", FileMode.Open);
            GameSaveData data = (GameSaveData)bf.Deserialize(file);
            file.Close();

            return data;
        }
            
        Debug.LogError("There is no save data!");
        return null;
    }
    public GameSaveData LoadLastGame()
    {
        var saveFiles = Directory.GetFiles(fileLocation + "/UnityProjectSaves/");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   File.Open(saveFiles[0], FileMode.Open);
        GameSaveData data = (GameSaveData)bf.Deserialize(file);
        file.Close();

        return data;
    }
}

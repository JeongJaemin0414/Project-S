using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public InventoryData inventoryData;
    public CharacterData characterData;
    public TimeData timeData;
}

public static class JsonSystem
{
    public static string Path => Application.persistentDataPath + "/datas/";
    public static string FileName => "saveData.json";

    public static void Save(SaveData saveData)
    {
        if (!Directory.Exists(Path))
            Directory.CreateDirectory(Path);

        string _jsonSaveData = JsonUtility.ToJson(saveData);

        string _filePath = Path + FileName;

        File.WriteAllText(_filePath, _jsonSaveData);
        Debug.Log("Save Success : " + _filePath);
    }

    public static SaveData Load()
    {
        string _filePath = Path + FileName;

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No Such saveFile exists");
            return null;
        }

        string _saveFile = File.ReadAllText(_filePath);
        SaveData _saveData = JsonUtility.FromJson<SaveData>(_saveFile);

        return _saveData;
    }
}

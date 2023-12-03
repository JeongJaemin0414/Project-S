using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CharacterData
{
    public string name;
    public int age;
    public int level;
}

[Serializable]
public struct InventoryData
{
    public int inventorySize;
}

[Serializable]
public struct TimeData
{
    public SeasonType seasonType;
    public int day;
    public int time;
}
   

public class DataSaveLoad : MonoBehaviour
{
    private CharacterData characterData;
    private InventoryData inventoryData;

    public CharacterData CharacterData
    {
        get { return characterData; }
    }

    public InventoryData InventoryData
    {
        get { return inventoryData; }
    }

    public void DataSave()
    {
        characterData.name = "Hello";
        characterData.age = 18;
        characterData.level = 10;

        inventoryData.inventorySize = 100;

        SaveData saveData = new()
        {
            characterData = characterData,
            inventoryData = inventoryData,
            timeData = TimeManager.Instance.TimeData,
    };

        JsonSystem.Save(saveData);
    }

    public void DataLoad()
    {
        SaveData loadData = JsonSystem.Load();

        characterData = loadData.characterData;
        inventoryData = loadData.inventoryData;
        TimeManager.Instance.TimeData = loadData.timeData;
    }
}

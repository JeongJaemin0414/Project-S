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


public class DataSaveLoad : MonoBehaviour
{

    public void InitSaveData()
    {
        SaveData saveData = new()
        {
            inventoryData = new(),
            timeData = new()
        };

        saveData.inventoryData.inventorySize = 16;
        saveData.inventoryData.inventoryitemDatas = new InventoryItemData[saveData.inventoryData.inventorySize];

        saveData.timeData.seasonType = SeasonType.spring;
        saveData.timeData.day = 0;
        saveData.timeData.time = 0;

        JsonSystem.Save(saveData);
    }

    public void DataSave()
    {
        //characterData.name = "Hello";
        //characterData.age = 18;
        //characterData.level = 10;

        SaveData saveData = new()
        {
            //characterData = characterData,
            inventoryData = InventoryManager.Instance.InventoryData,
            timeData = TimeManager.Instance.TimeData,

        };

        JsonSystem.Save(saveData);
    }

    public void DataLoad()
    {
        SaveData loadData = JsonSystem.Load();

        //characterData = loadData.characterData;
        InventoryManager.Instance.InventoryData = loadData.inventoryData;
        TimeManager.Instance.TimeData = loadData.timeData;
    }
}

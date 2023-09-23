using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterData
{
    public string name;
    public int age;
    public int level; 
}

[System.Serializable]
public struct InventoryData
{
    public int inventorySize;
}

public class GameManager : Singleton<GameManager>
{
    private CharacterData characterData;
    public CharacterData CharacterData
    {
        get { return characterData; }
        set { characterData = value; }
    }

    private InventoryData inventoryData;
    public InventoryData InventoryData
    {
        get { return inventoryData; }
        set { inventoryData = value; }
    }

    public bool isPlayerStop = false;

    protected override void Awake()
    {
        DataSave();

        DataLoad();
    }

    public void SetPlayerStop(bool isStop)
    {
        isPlayerStop = isStop;
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
        };

        JsonSystem.Save(saveData);
    }

    public void DataLoad()
    {
        SaveData loadData = JsonSystem.Load();

        characterData = loadData.characterData;
        inventoryData = loadData.inventoryData;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct InventoryData
{
    public int inventorySize;
    public InventoryItemData[] inventoryitemDatas;
}

[Serializable]
public struct InventoryItemData
{
    public int itemIndex;
    public int itemCount;
}

public class InventoryManager : Singleton<InventoryManager>
{
    private InventoryData inventoryData;
    public InventoryData InventoryData 
    {
        get => inventoryData; 
        set => inventoryData = value; 
    }

    public override void Init()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            InventoryItemData inventoryItemData = new();
            inventoryItemData.itemIndex = 1020000;
            inventoryItemData.itemCount = 1;

            AddInventoryItemData(inventoryItemData);
        }
    }

    public bool IsEmptyInventory(InventoryItemData inventoryItemData)
    {
        return (inventoryItemData.itemIndex == 0 && inventoryItemData.itemCount == 0);
    }

    public void ChangeInventoryItemData(int oldInvenIndex, int newInvenIndex, InventoryItemData newInventoryItemData)
    {
        InventoryItemData oldInventoryData = inventoryData.inventoryitemDatas[oldInvenIndex];
        InventoryItemData newInventoryData = inventoryData.inventoryitemDatas[newInvenIndex];

        if(IsEmptyInventory(newInventoryData))
        {
            inventoryData.inventoryitemDatas[newInvenIndex] = oldInventoryData;
            oldInventoryData.itemIndex = 0;
            oldInventoryData.itemCount = 0;
            inventoryData.inventoryitemDatas[oldInvenIndex] = oldInventoryData;
        }
        else
        {
            if (newInventoryData.itemIndex == oldInventoryData.itemIndex)
            {
                int newItemCount = newInventoryData.itemCount + oldInventoryData.itemCount;
                ItemData newItemData = ItemManager.Instance.GetItemData(newInventoryData.itemIndex);

                if (newItemCount > newItemData.invenMaxCount)
                {
                    inventoryData.inventoryitemDatas[newInvenIndex].itemCount = oldInventoryData.itemCount;
                    inventoryData.inventoryitemDatas[oldInvenIndex].itemCount = newInventoryData.itemCount;
                }
                else
                {
                    inventoryData.inventoryitemDatas[newInvenIndex].itemCount = newItemCount;
                    oldInventoryData.itemIndex = 0;
                    oldInventoryData.itemCount = 0;
                    inventoryData.inventoryitemDatas[oldInvenIndex] = oldInventoryData;
                }
            }
        }

        UIManager.Instance.RefreshInventory();
    }

    public void AddInventoryItemData(InventoryItemData newInventoryItemData)
    {
        for(int i = 0; i < inventoryData.inventoryitemDatas.Length; i++) //find item Index
        {
            InventoryItemData inventoryItemData = inventoryData.inventoryitemDatas[i];

            if(inventoryItemData.itemIndex == newInventoryItemData.itemIndex)
            {
                int newItemCount = inventoryItemData.itemCount + newInventoryItemData.itemCount;
                int maxItemCount = ItemManager.Instance.GetItemData(inventoryItemData.itemIndex).invenMaxCount;

                if (newItemCount <= maxItemCount)
                {
                    inventoryData.inventoryitemDatas[i].itemCount = newItemCount;
                    UIManager.Instance.RefreshInventory();

                    return;
                }
            }
        }

        for (int i = 0; i < inventoryData.inventoryitemDatas.Length; i++)
        {
            InventoryItemData inventoryItemData = inventoryData.inventoryitemDatas[i];

            if(IsEmptyInventory(inventoryItemData))
            {
                inventoryData.inventoryitemDatas[i] = newInventoryItemData;
                UIManager.Instance.RefreshInventory();

                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemData
{
    public string name;
    public string desc;
    public ItemType itemType;
    public int[] craftMaterial;
    public int[] materialValue;
    public int buyGold;
    public bool salePossible;
    public int saleGold;
}

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<int, ItemData> itemDatas = new();
    public override void Init()
    {

    }

    public void InitItemData()
    {

    }

    public void CreateItem(int itemIndex, Vector3 itemPos)
    {
        //itemTable item = ExcelManager.Instance.GetExcelData<itemTable>().item



    }

    public ItemData GetItem(int itemIndex)
    {
        if (itemDatas.TryGetValue(itemIndex, out ItemData itemData))
        {
            return itemData;
        }

        return GetItemDataFromTable(itemIndex);
    }

    private ItemData GetItemDataFromTable(int itemIndex)
    {
        ItemTableEntity itemTableEntity = ExcelManager.Instance.GetExcelData<ItemTable>().item.Find(x => x.index == itemIndex);

        ItemData itemData = new()
        {
            name = LanguageManager.Instance.GetString(itemTableEntity.name),
            desc = LanguageManager.Instance.GetString(itemTableEntity.desc),
            itemType = (ItemType)itemTableEntity.itemType,
            craftMaterial = Utilities.GetArrayDataInt(itemTableEntity.craftMaterial),
            materialValue = Utilities.GetArrayDataInt(itemTableEntity.materialValue),
            buyGold = itemTableEntity.buyGold,
            salePossible = itemTableEntity.salePossible,
            saleGold = itemTableEntity.saleGold
        };

        itemDatas.Add(itemIndex, itemData); 
        return itemData;
    }
}

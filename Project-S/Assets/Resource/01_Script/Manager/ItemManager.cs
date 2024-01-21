using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//여기 있던 ItemData 별도 파일로 분리해서 이동.

public class ItemManager : Singleton<ItemManager>
{
    private Dictionary<int, ItemData> itemDatas = new();
    public override void Init()
    {

    }

    public void CreateItem(int itemIndex, Vector3 itemPos)
    {
        ItemData itemData = GetItemData(itemIndex);

        ItemBase item = AddressbleManager.Instance.LoadAsset<GameObject>(itemData.modelResourceName).GetComponent<ItemBase>();
        item.Itemindex = itemIndex;
        item.transform.position = itemPos;
    }

    public ItemData GetItemData(int itemIndex)
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
            //name = LanguageManager.Instance.GetString(itemTableEntity.name),
            //desc = LanguageManager.Instance.GetString(itemTableEntity.desc),
            iconResourceName = itemTableEntity.iconResourceName,
            modelResourceName = itemTableEntity.modelResourceName,
            itemType = (ItemType)itemTableEntity.itemType,
            craftMaterial = Utilities.GetArrayDataInt(itemTableEntity.craftMaterial),
            materialValue = Utilities.GetArrayDataInt(itemTableEntity.materialValue),
            buyGold = itemTableEntity.buyGold,
            salePossible = itemTableEntity.salePossible,
            saleGold = itemTableEntity.saleGold,
            invenMaxCount = itemTableEntity.invenMaxCount,
        };

        itemDatas.Add(itemIndex, itemData); 
        return itemData;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public struct InventoryGroupUI
{
    public InventoryItemType inventoryItemType;
    public GameObject inventoryGroup;
    public List<ItemUI> items;
}

public class InventorySystem : UISystemBase
{
    public List<InventoryGroupUI> inventoryGroupUIs;
    public override void Init()
    {
        foreach(InventoryGroupUI inventoryGroupUI in inventoryGroupUIs)
        {
            for(int i = 0; i < inventoryGroupUI.items.Count; i++)
            {
                inventoryGroupUI.items[i].SetItemUI(ItemType.None, "", 0);
            }
        }
    }

    public void OnClickInventoryItemTypeBtn(InventoryItemTypeComponent inventoryItemTypeComponent)
    {
        foreach(InventoryGroupUI inventoryGroupUI in inventoryGroupUIs)
        {
            inventoryGroupUI.inventoryGroup.SetActive(false);
        }

        InventoryGroupUI _inventoryGroupUI = inventoryGroupUIs.Find(x => x.inventoryItemType == inventoryItemTypeComponent.inventoryItemType);
        _inventoryGroupUI.inventoryGroup.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public struct InventoryGroupUI
{
    public InventoryItemType inventoryItemType;
    public GameObject inventoryGroup;
    public InventorySlot[] inventorySlots;
}

public class InventorySystem : UISystemBase
{
    public InventoryGroupUI inventoryGroupUIs;
    public override void Init()
    {
        for(int i = 0; i < inventoryGroupUIs.inventorySlots.Length; i++)
        {
            inventoryGroupUIs.inventorySlots[i].InvenSlotIndex = i;
        }

        RefreshInventory();
    }

    public void RefreshInventory()
    {
        for(int i = 0; i < inventoryGroupUIs.inventorySlots.Length; i++)
        {
            InventorySlot inventorySlot = inventoryGroupUIs.inventorySlots[i];
            InventoryItemData inventoryItemData = InventoryManager.Instance.InventoryData.inventoryitemDatas[i];
            
            if(inventorySlot != null)
            {
                inventorySlot.SetInventoryItem(inventoryItemData);
            }
        }
    }

    public void OnClickInventoryItemTypeBtn(InventoryItemTypeComponent inventoryItemTypeComponent)
    {
        //foreach(InventoryGroupUI inventoryGroupUI in inventoryGroupUIs)
        //{
        //    inventoryGroupUI.inventoryGroup.SetActive(false);
        //}

        //InventoryGroupUI _inventoryGroupUI = inventoryGroupUIs.Find(x => x.inventoryItemType == inventoryItemTypeComponent.inventoryItemType);
        //_inventoryGroupUI.inventoryGroup.SetActive(true);
    }

}

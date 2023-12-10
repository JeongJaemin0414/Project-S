using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int invenSlotIndex;
    public int InvenSlotIndex { get => invenSlotIndex; set => invenSlotIndex = value; }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        InventorySlot oldInventorySlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

        if (transform.childCount == 0)
        {
            inventoryItem.parentAfterDrag = transform;
        }

        inventoryItem.itemImage.raycastTarget = true;
        inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);

        if (oldInventorySlot.invenSlotIndex != invenSlotIndex)
        {
            InventoryManager.Instance.ChangeInventoryItemData(oldInventorySlot.invenSlotIndex, invenSlotIndex, inventoryItem.InventoryItemData);
        }
    }

    public void SetInventoryItem(InventoryItemData inventoryItemData)
    {
        InventoryItem inventoryItem = GetComponentInChildren<InventoryItem>();

        if(inventoryItem == null)
        {
            if (!InventoryManager.Instance.IsEmptyInventory(inventoryItemData))
            {
                inventoryItem = AddressbleManager.Instance.LoadAsset<GameObject>("InventoryItem", transform).GetComponent<InventoryItem>();
                ItemData itemData = ItemManager.Instance.GetItemData(inventoryItemData.itemIndex);
                inventoryItem.SetItemUI(inventoryItemData, itemData);
            }
        }
        else
        {
            if (InventoryManager.Instance.IsEmptyInventory(inventoryItemData))
            {
                Destroy(inventoryItem.gameObject);
                return;
            }
            else
            {
                ItemData itemData = ItemManager.Instance.GetItemData(inventoryItemData.itemIndex);
                inventoryItem.SetItemUI(inventoryItemData, itemData);
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int invenSlotIndex;
    public int InvenSlotIndex { get => invenSlotIndex; set => invenSlotIndex = value; }

    [SerializeField]
    private InventoryItem currentInventoryItem = null;
    public InventoryItem CurrentInventoryItem { get => currentInventoryItem; }
    public void OnDrop(PointerEventData eventData)
    {
        //InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        //InventorySlot oldInventorySlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

        //if (transform.childCount == 0)
        //{
        //    inventoryItem.parentAfterDrag = transform;
        //}

        //inventoryItem.itemImage.raycastTarget = true;
        //inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);

        //if (oldInventorySlot.invenSlotIndex != invenSlotIndex)
        //{
        //    InventoryManager.Instance.ChangeInventoryItemData(oldInventorySlot.invenSlotIndex, invenSlotIndex);
        //}
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        inventoryItem.itemImage.raycastTarget = true;
        inventoryItem.transform.SetParent(transform);

        InventoryManager.Instance.RefreshInventoryData();
    }

    public void ChangeInventoryItem(InventoryItem inventoryItem)
    {
        InventorySlot oldInventorySlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

        inventoryItem.itemImage.raycastTarget = true;
        inventoryItem.transform.SetParent(transform);

        if (oldInventorySlot == null)
        {
            currentInventoryItem = inventoryItem;

            InventoryManager.Instance.SetInventoryItemData(invenSlotIndex, inventoryItem.InventoryItemData);
        }
        else
        {
            if (oldInventorySlot.invenSlotIndex != invenSlotIndex)
            {
                InventoryManager.Instance.ChangeInventoryItemData(oldInventorySlot.invenSlotIndex, invenSlotIndex);
            }
        }
    }

    public void SetInventoryItem(InventoryItem inventoryItem)
    {
        InventorySlot oldInventorySlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

        inventoryItem.itemImage.raycastTarget = true;
        inventoryItem.transform.SetParent(transform);

        if (oldInventorySlot == null)
        {
            InventoryManager.Instance.SetInventoryItemData(invenSlotIndex, inventoryItem.InventoryItemData);
        }
        else
        {
            InventoryManager.Instance.SetInventoryItemData(oldInventorySlot.InvenSlotIndex, invenSlotIndex, inventoryItem.InventoryItemData);
        }
    }

    public InventoryItem RefreshInventoryItem(InventoryItemData inventoryItemData)
    {
        currentInventoryItem = gameObject.GetComponentInChildren<InventoryItem>();

        if (currentInventoryItem == null)
        {
            if (!InventoryManager.Instance.IsEmptyInventory(inventoryItemData))
            {
                currentInventoryItem = AddressbleManager.Instance.LoadAsset<GameObject>("InventoryItem", transform).GetComponent<InventoryItem>();
                currentInventoryItem.SetItemUI(inventoryItemData);
            }
        }
        else
        {
            if (InventoryManager.Instance.IsEmptyInventory(inventoryItemData))
            {
                Destroy(currentInventoryItem.gameObject);
                currentInventoryItem = null;
            }
            else
            {
                currentInventoryItem.SetItemUI(inventoryItemData);
            }
        }

        return currentInventoryItem;
    }

}

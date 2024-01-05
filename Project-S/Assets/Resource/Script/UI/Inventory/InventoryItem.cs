using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryItem : MonoBehaviour
{
    private InventoryItemData inventoryItemData;
    public InventoryItemData InventoryItemData { get => inventoryItemData; }

    public Image itemImage;
    public TextMeshProUGUI itemCount;

    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(Transform parent = null)
    {
        itemImage.raycastTarget = false;
        parentAfterDrag = (parent == null) ? transform.parent : parent;
        transform.SetParent(transform.root);
    }

    public void OnEndDrag()
    {
        itemImage.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void SetItemUI(InventoryItemData newInventoryItemData)
    {
        inventoryItemData = newInventoryItemData;

        ItemData newitemData = ItemManager.Instance.GetItemData(inventoryItemData.itemIndex);

        string itemResourceName = newitemData.iconResourceName;
        int itemCountValue = inventoryItemData.itemCount;

        AddressbleManager.Instance.SetSprite(itemImage, itemResourceName);
        itemImage.gameObject.SetActive(true);

        if (itemCountValue > 1)
        {
            itemCount.gameObject.SetActive(true);
            itemCount.text = itemCountValue.ToString();
        }
        else
            itemCount.gameObject.SetActive(false);
    }


}

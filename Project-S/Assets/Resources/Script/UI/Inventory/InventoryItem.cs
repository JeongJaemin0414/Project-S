using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private InventoryItemData inventoryItemData;
    public InventoryItemData InventoryItemData { get => inventoryItemData; }

    public Image itemImage;
    public TextMeshProUGUI itemCount;

    [HideInInspector] public Transform parentAfterDrag;

    public Action<int, int, InventoryItemData> OnEndDragAction;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    public void SetItemUI(InventoryItemData newInventoryItemData, ItemData newitemData)
    {
        inventoryItemData = newInventoryItemData;

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

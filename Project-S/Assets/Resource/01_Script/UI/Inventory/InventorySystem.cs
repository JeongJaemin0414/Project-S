using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public struct InventoryGroupUI
{
    public InventoryItemType inventoryItemType;
    public GameObject inventoryGroup;
    public List<InventorySlot> inventorySlots;
}

public class InventorySystem : UISystemBase, IPointerDownHandler, IPointerUpHandler
{
    public InventoryGroupUI inventoryGroupUIs;
    private InventoryItem draggItem = null;

    private bool isRightClickDown = false;
    private float isClickTime = 0f;
    private InventoryItem rightClickedInventoryItem = null;

    public override void Init()
    {
        for(int i = 0; i < inventoryGroupUIs.inventorySlots.Count; i++)
        {
            inventoryGroupUIs.inventorySlots[i].InvenSlotIndex = i;
        }

        RefreshInventory();
    }

    public void Update()
    {
        if (draggItem != null)
        {
            draggItem.transform.position = Input.mousePosition;
        }
    }

    public void RefreshInventory()
    {
        for(int i = 0; i < inventoryGroupUIs.inventorySlots.Count; i++)
        {
            InventorySlot inventorySlot = inventoryGroupUIs.inventorySlots[i];
            InventoryItemData inventoryItemData = InventoryManager.Instance.InventoryData.inventoryitemDatas[i];
            
            if(inventorySlot != null)
            {
                inventorySlot.RefreshInventoryItem(inventoryItemData);
            }
        }
    }

    public InventoryItem CreateInventoryItem(InventoryItemData inventoryItemData)
    {
        InventoryItem newInventoryItem = AddressbleManager.Instance.LoadAsset<GameObject>("InventoryItem", transform.root).GetComponent<InventoryItem>();

        if(newInventoryItem != null)
        {
            newInventoryItem.gameObject.transform.position = Input.mousePosition;
            newInventoryItem.SetItemUI(inventoryItemData);
        }

        return newInventoryItem;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot inventorySlot = clickObject.GetComponent<InventorySlot>();
            InventoryItem inventoryItem = clickObject.GetComponent<InventoryItem>();

            if (draggItem == null && inventoryItem != null)
            {
                draggItem = inventoryItem;
                draggItem.OnBeginDrag();
            }
            else if (draggItem != null && inventorySlot != null)
            {
                inventorySlot.SetItem(draggItem);
                draggItem = null;
            }
            else if (draggItem != null && inventoryItem != null)
            {
                inventorySlot = inventoryItem.transform.parent.GetComponent<InventorySlot>();

                if (draggItem.InventoryItemData.itemIndex != inventoryItem.InventoryItemData.itemIndex)
                {
                    InventoryItem tempInventoryItem = draggItem;

                    draggItem = inventoryItem;
                    draggItem.OnBeginDrag();

                    inventorySlot.SetItem(tempInventoryItem);
                }
                else
                {
                    int newItemCount = draggItem.InventoryItemData.itemCount + inventoryItem.InventoryItemData.itemCount;
                    int invenMaxCount = ItemManager.Instance.GetItemData(inventoryItem.InventoryItemData.itemIndex).invenMaxCount;

                    if (inventoryItem.InventoryItemData.itemCount == invenMaxCount)
                    {
                        InventoryItem tempInventoryItem = draggItem;

                        draggItem = inventoryItem;
                        draggItem.OnBeginDrag();

                        inventorySlot.SetItem(tempInventoryItem);
                    }
                    else if (newItemCount > invenMaxCount)
                    {
                        InventoryItemData newInventoryItemData = new()
                        {
                            itemIndex = inventoryItem.InventoryItemData.itemIndex,
                            itemCount = invenMaxCount,
                        };

                        InventoryItemData draggInventoryItemData = new()
                        {
                            itemIndex = draggItem.InventoryItemData.itemIndex,
                            itemCount = newItemCount - invenMaxCount,
                        };

                        inventoryItem.SetItemUI(newInventoryItemData);
                        draggItem.SetItemUI(draggInventoryItemData);

                        inventorySlot.SetItem(inventoryItem);
                    }
                    else
                    {
                        InventoryItemData newInventoryItemData = new()
                        {
                            itemIndex = inventoryItem.InventoryItemData.itemIndex,
                            itemCount = newItemCount,
                        };

                        inventoryItem.SetItemUI(newInventoryItemData);
                        inventorySlot.SetItem(inventoryItem);

                        Destroy(draggItem.gameObject);
                        draggItem = null;
                    }
                }
            }
            else if (draggItem != null && inventoryItem == null && inventorySlot == null)
            {
                inventoryItem = draggItem.parentAfterDrag.GetComponentInChildren<InventoryItem>();

                if(inventoryItem == null)
                {
                    draggItem.OnEndDrag();
                }
                else
                {
                    inventorySlot = inventoryItem.transform.parent.GetComponent<InventorySlot>();

                    InventoryItemData newInventoryItemData = new()
                    {
                        itemIndex = inventoryItem.InventoryItemData.itemIndex,
                        itemCount = draggItem.InventoryItemData.itemCount + inventoryItem.InventoryItemData.itemCount,
                    };

                    inventoryItem.SetItemUI(newInventoryItemData);
                    inventorySlot.SetItem(inventoryItem);

                    Destroy(draggItem.gameObject);
                }

                draggItem = null;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject clickObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlot inventorySlot = clickObject.GetComponent<InventorySlot>();
            InventoryItem inventoryItem = clickObject.GetComponent<InventoryItem>();

            if(inventorySlot)
            {
                if(draggItem != null)
                {
                    inventorySlot.SetItem(draggItem);
                    draggItem = null;
                }
            }
            else if(inventoryItem)
            {
                isRightClickDown = true;
                rightClickedInventoryItem = inventoryItem;

                IsRightClick();

                TimeManager.Instance.AddTimer(1, () =>
                {
                    if (isRightClickDown)
                        StartCoroutine(PressedRightClick());
                });
            }
        }
    }

    public bool IsRightClick()
    {
        if (rightClickedInventoryItem == null || 
            (draggItem != null && (draggItem.InventoryItemData.itemCount >= ItemManager.Instance.GetItemData(draggItem.InventoryItemData.itemIndex).invenMaxCount)))
        {
            return false;
        }

        InventoryItemData newInventoryItemData = new()
        {
            itemIndex = rightClickedInventoryItem.InventoryItemData.itemIndex,
            itemCount = rightClickedInventoryItem.InventoryItemData.itemCount - 1,
        };

        InventoryItemData draggInventoryItemData = new()
        {
            itemIndex = rightClickedInventoryItem.InventoryItemData.itemIndex,
            itemCount = (draggItem == null) ? 1 : draggItem.InventoryItemData.itemCount + 1,
        };

        rightClickedInventoryItem.SetItemUI(newInventoryItemData);

        if (draggItem == null)
        {
            draggItem = CreateInventoryItem(draggInventoryItemData);
            draggItem.OnBeginDrag(rightClickedInventoryItem.transform.parent);
        }
        else
        {
            rightClickedInventoryItem.SetItemUI(newInventoryItemData);
            draggItem.SetItemUI(draggInventoryItemData);
        }

        if (rightClickedInventoryItem.InventoryItemData.itemCount < 1)
        {
            Destroy(rightClickedInventoryItem.gameObject);
            rightClickedInventoryItem = null;
            return false;
        }

        return true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(PressedRightClick());

        isRightClickDown = false;
        rightClickedInventoryItem = null;
    }

    public IEnumerator PressedRightClick()
    {
        while (IsRightClick())
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

}

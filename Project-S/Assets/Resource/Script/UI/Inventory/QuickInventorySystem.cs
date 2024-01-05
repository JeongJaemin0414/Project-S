using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public struct QuickInventoryUI 
{
    public TextMeshProUGUI lineCountText;
    public List<InventorySlot> inventorySlots;
}

public class QuickInventorySystem : UISystemBase
{
    public QuickInventoryUI quickInventoryUI;

    private int quickInventoryLineCount = 0;
    private int currentslotIndex = 0;

    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
    };

    public override void Init()
    {
        for(int i = 0; i < quickInventoryUI.inventorySlots.Count; i++)
        {
            Button slotBtn = quickInventoryUI.inventorySlots[i].gameObject.AddComponent<Button>();
            int slotIndex = i;
            slotBtn.onClick.AddListener(() => { OnClickSlot(slotIndex); });
        }

        SetQuickInventory();
        OnClickSlot(currentslotIndex);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            quickInventoryLineCount = (quickInventoryLineCount - 1 + 2) % 2;
            quickInventoryUI.lineCountText.text = quickInventoryLineCount.ToString();
            SetQuickInventory(quickInventoryLineCount);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            quickInventoryLineCount = (quickInventoryLineCount + 1) % 2;
            quickInventoryUI.lineCountText.text = quickInventoryLineCount.ToString();
            SetQuickInventory(quickInventoryLineCount);
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int numberPressed = i;
                OnClickSlot(numberPressed);
            }
        }

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (wheelInput > 0)
        {
            OnClickSlot((currentslotIndex - 1 + 10) % 10);
        }
        else if (wheelInput < 0)
        {
            OnClickSlot((currentslotIndex + 1) % 10);
        }

    }

    public void OnClickSlot(int slotIndex)
    {
        Image oldSlotImage = quickInventoryUI.inventorySlots[currentslotIndex].GetComponent<Image>();
        oldSlotImage.color = new Color(0.5f, 0.5f, 0.5f, 1);

        currentslotIndex = slotIndex;

        Image newSlotImage = quickInventoryUI.inventorySlots[currentslotIndex].GetComponent<Image>();
        newSlotImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }

    public void SetQuickInventory()
    {
        SetQuickInventory(quickInventoryLineCount);
    }

    public void SetQuickInventory(int lineCount)
    {
        quickInventoryLineCount = lineCount;
        InventoryItemData[] inventoryItemDatas = InventoryManager.Instance.GetInventoryLineData(quickInventoryLineCount);

        for(int i = 0; i < inventoryItemDatas.Length; i++)
        {
            InventorySlot inventorySlot = quickInventoryUI.inventorySlots[i];
            InventoryItemData inventoryItemData = inventoryItemDatas[i];

            InventoryItem inventoryItem = inventorySlot.RefreshInventoryItem(inventoryItemData);
            
            if(inventoryItem != null)
                inventoryItem.itemImage.raycastTarget = false;
        }
    }
}

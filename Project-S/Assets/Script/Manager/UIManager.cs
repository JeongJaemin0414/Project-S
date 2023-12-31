using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] 
    private DialogSystem dialogSystem;

    [SerializeField]
    private InventorySystem inventorySystem;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Start()
    {
        dialogSystem.Init();
        inventorySystem.Init();
    }

    public override void Init()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OpenDialog(101);
        }   
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            OpenInventory();
        }
    }

    public void OpenDialog(int groupIndex)
    {
        if (!dialogSystem.IsOpening())
        {
            dialogSystem.OpenUISystem();
            dialogSystem.SetDialogData(groupIndex);
            dialogSystem.UpdateDioalog();
        }
    }

    public void OpenInventory()
    {
        if (!inventorySystem.IsOpening())
        {
            inventorySystem.OpenUISystem();
        }
        else
        {
            inventorySystem.CloseUISystem();
        }
    }

    public void SetTimerText(string time)
    {
        timerText.text = time;
    }
}

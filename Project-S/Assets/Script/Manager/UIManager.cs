using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] 
    private DialogSystem dialogSystem;

    public void Start()
    {
        OpenDialog(101);
    }

    public void OpenDialog(int groupIndex)
    {
        dialogSystem.SetDialogData(groupIndex);
        dialogSystem.UpdateDioalog();
        dialogSystem.OpenDialogPanel();
    }
}

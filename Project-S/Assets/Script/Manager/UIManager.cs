using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] 
    private DialogSystem dialogSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OpenDialog(101);
        }    
    }

    public void OpenDialog(int groupIndex)
    {
        if (!dialogSystem.IsDialoging())
        {
            dialogSystem.OpenDialogPanel();
            dialogSystem.SetDialogData(groupIndex);
            dialogSystem.UpdateDioalog();
        }
    }
}

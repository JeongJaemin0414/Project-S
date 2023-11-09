using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ExcelManager : Singleton<ExcelManager>
{
    [SerializeField]
    private List<ExcelBase> ExcelList;

    public void Start()
    {
        string q = "1";
        string w = "2";
        
        



        foreach (float i in Utilities.GetArrayDataFloat(GetExcelData<TimeTable>().time[0].weatherPercent))
        {
            Debug.Log(i);
        }
    }

    public override void Init()
    {
    }

    public T GetExcelData<T>() where T : ExcelBase
    {
        List<T> matchingItems = ExcelList.OfType<T>().ToList();

        if (matchingItems.Count == 0)
        {
            return default; 
        }

        return matchingItems[0];
    }
}

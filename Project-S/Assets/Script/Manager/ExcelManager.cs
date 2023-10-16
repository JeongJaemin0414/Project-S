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
        foreach (int i in GetArrayData(GetExcelData<TimeTable>().time[0].weatherType))
        {
            Debug.Log(i);
        }
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

    public int[] GetArrayData(string s1)
    {
        int[] iq = s1.Split(';').Select(n => Convert.ToInt32(n)).ToArray();
        return iq;
    }
}

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
        foreach (float i in GetArrayDataFloat(GetExcelData<TimeTable>().time[0].weatherPercent))
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

    public int[] GetArrayDataInt(string s1)
    {
        int[] iq = s1.Split(';').Select(n => Convert.ToInt32(n)).ToArray();
        return iq;
    }

    public float[] GetArrayDataFloat(string s1)
    {
        float[] fq = s1.Split(';').Select(n => Convert.ToSingle(n)).ToArray();
        return fq;
    }
}

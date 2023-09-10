using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExcelManager : Singleton<ExcelManager>
{
    [SerializeField]
    private List<ExcelBase> ExcelList;

    public void Awake()
    {
        Eghl();
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

    public void Eghl()
    {
        TestExcel testExcel = ExcelManager.Instance.GetExcelData<TestExcel>();
        Debug.Log(testExcel.Entities[0].index);
        Debug.Log(testExcel.Entities[0].name);
        Debug.Log(testExcel.Entities[0].age);
    }
}

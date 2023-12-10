using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private DataSaveLoad dataSaveLoad;

    public bool isPlayerStop = false;

    protected override void Awake()
    {
        dataSaveLoad = GetComponent<DataSaveLoad>();
    }

    private void Start()
    {
        DataLoad();

        Init();
    }

    public override void Init()
    {
        TimeManager.Instance.Init();
        AddressbleManager.Instance.Init();
        DOTweenManager.Instance.Init();
        LanguageManager.Instance.Init();
        CharacterManager.Instance.Init();
        InventoryManager.Instance.Init();
        ExcelManager.Instance.Init();  
        FarmManager.Instance.Init();
        UIManager.Instance.Init();
    }

    public void SetPlayerStop(bool isStop)
    {
        isPlayerStop = isStop;
    }

    public void InitSaveData()
    {
        dataSaveLoad.InitSaveData();
    }

    public void DataSave()
    {
        dataSaveLoad.DataSave();
    }

    public void DataLoad()
    {
        dataSaveLoad.DataLoad();
    }
}
